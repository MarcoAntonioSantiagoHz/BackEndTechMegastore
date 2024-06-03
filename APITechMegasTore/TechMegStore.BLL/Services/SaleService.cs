using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



//Referencias
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechMegStore.BLL.Services.Interface;
using TechMegStore.DATAS.Repositories.Interfaces;
using TechMegStore.DTO;
using TechMegStore.Models;



namespace TechMegStore.BLL.Services
{ //Clase  que crea Herencia de la interfaz ISaleService
    public class SaleService : ISaleService
    {
        //Clase que implementara los metodos de la interfaz IProductService



        //Creamos dos variables privadas de tipo lectura
        private readonly ISaleRepository _saleRepository;
        private readonly IGenericRepository<SaleDetail> _saleDetailRepository;
        private readonly IMapper _mapper;

        //Generamos constructor con parametros

        public SaleService(ISaleRepository saleRepository,
        IGenericRepository<SaleDetail> saleDetailRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _saleDetailRepository = saleDetailRepository;
            _mapper = mapper;
        }



        //Creacion de metodos


        //Metodo de registrar 

        public async Task<SaleDTO> Register(SaleDTO model)
        {
            try
            {
                //Variable de venta generada
                var saleGenerated = await _saleRepository.Register(_mapper.Map<Sale>(model));
                if (saleGenerated.IdSale == 0)
                {
                    //Devuelve el error con una exepcion
                    throw new TaskCanceledException("Im Sorry, Your Register No Create"); //No se puede crear
                }

                return _mapper.Map<SaleDTO>(saleGenerated);
            }
            catch
            {
                throw;
            }
        }

        //Metodo de Historial De Venta 


        public async Task<List<SaleDTO>> SalesHistory(string searchFor, string numberSale, string startDate, string dateEnd)
        {

            //Creamos una Query, ya que consultamos a la tabla venta "Sale" y hace  "CONSULTAR GetAllConsult" a esa tabla

            IQueryable<Sale> query = await _saleRepository.GetAllConsult();

            var listResult = new List<Sale>();

            try
            {
                //VALIDAMOS LA BUSQUEDA,
                //Buscar Por fecha "Date"
                if (searchFor == "Date")
                {
                    //POR SI SE BUSCA HISTORIAL POR FECHA

                    // Convertirmos los parametros de formato String fecha de inicio y fecha de fin a DateTime
                    DateTime start_dateParsed = DateTime.ParseExact(startDate, "dd/MM/yyyy", new CultureInfo("es-MX"));
                    DateTime end_datParsed = DateTime.ParseExact(dateEnd, "dd/MM/yyyy", new CultureInfo("es-MX"));

                    //La letra "V" es solo una variable para aplicar los filtrs
                    listResult = await query.Where(v =>
                        v.DateRegistration.Value.Date >= start_dateParsed.Date &&
                        v.DateRegistration.Value.Date <= end_datParsed.Date).
                        //Include para obtener el detalle de venta
                        Include(dv => dv.SaleDetails)
                        //Los productos por cada detalle de venta
                        .ThenInclude(p => p.IdProductNavigation).ToListAsync();

                }
                else
                {
                    //Por numero de venta
                    // La letra "V" es solo una variable para aplicar los filtrs
                    listResult = await query.Where(v =>
                     //Aplicamos los filtros
                     //Numero de documento tiene que ser igual al parametro que se le paso de numero de venta
                     v.DocumentNumber == numberSale).
                        //Include para obtener el detalle de venta
                        Include(dv => dv.SaleDetails)
                        //Los productos por cada detalle de venta
                        .ThenInclude(p => p.IdProductNavigation).ToListAsync();
                }
            }
            catch
            {
                throw;
            }

            //Retornamos esa lista resultados y necesitamos mapperalo
            return _mapper.Map<List<SaleDTO>>(listResult);//Le pasamos la variable a convertir
        }


        //Metodo de Reportes 

        public async Task<List<ReportDTO>> Report(string startDate, string dateEnd)
        {

            //Creamos una Query, ya que consultamos detalle de venta con "GetAllConsult"
            IQueryable<SaleDetail> query = await _saleDetailRepository.GetAllConsult();
            var listResult = new List<SaleDetail>();

            try
            {

                // Convertirmos los parametros de formato String fecha de inicio y fecha de fin a DateTime
                DateTime start_dateParsed = DateTime.ParseExact(startDate, "dd/MM/yyyy", new CultureInfo("es-MX"));
                DateTime end_datParsed = DateTime.ParseExact(dateEnd, "dd/MM/yyyy", new CultureInfo("es-MX"));

                //Ejecutamos nuestra query
                listResult = await query
                    //Incluimos todo lo que necesitamos en este caso los productos, nuestra tabla de venta
                    .Include(products => products.IdProductNavigation)
                    .Include(sale => sale.IdSaleNavigation)
                //Por ultimo agrega el filtro
                //Estaremos trabajando con nuestra fecha de registro de nuestra venta
                .Where(dv =>
                       dv.IdSaleNavigation.DateRegistration.Value.Date >= start_dateParsed.Date &&
                       dv.IdSaleNavigation.DateRegistration.Value.Date <= end_datParsed.Date).ToListAsync();

            }
            catch
            {
                //En caso de que haya error  se abrira el throw
                throw;
            }

            //Retornables nuestra variable lista resultado pero lo vamos a convertir a un tipo de lista de nuestro reporte DTO
                return _mapper.Map<List<ReportDTO>>(listResult);
        }
    }
}
