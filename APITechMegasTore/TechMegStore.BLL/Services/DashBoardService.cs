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
{ //Clase  que crea Herencia de la interfaz IDashBoardService
    public class DashBoardService : IDashBoardService
    {




        //Clase que implementara los metodos de la interfaz IProductService


        //Creamos dos variables privadas de tipo lectura
        private readonly ISaleRepository _saleRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        //Generamos constructor, instanciando parametros
        public DashBoardService(ISaleRepository saleRepository, IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        //METODOS CREADOS MANUALMENTE QUE NOS AYUDARA A CREAR EL RESUMEN

        //METODO CREADO POR LA INTERFAZ
        public async Task<DashBoardDTO> Overview() //"Overview resumen"
        {
           
            DashBoardDTO vmDashBoard = new DashBoardDTO();

            try
            {
                //Queremos actuzalizar el vmDashBoardDTO
                vmDashBoard.SaleTotal = await SaleLastWeek();
                vmDashBoard.TotalIncome = await TotalIncome();
                vmDashBoard.TotalProducts = await TotalProducts();


                //Creamos una variable de tipo ventas dela semanaDTO
                List<SaleWeekDTO> listSaleWeek = new List<SaleWeekDTO>();

                //Para poder recorrer cada uno de nuestros elementos de nuestro Diccionario de ventas ultima semana
                foreach (KeyValuePair<string, int> item in await SalesLastWeek())//Devuelve "SalesLastWeek" porque es unalista de  diccionario
                {
                    listSaleWeek.Add(new SaleWeekDTO()
                    {
                        Date = item.Key,
                        Total = item.Value,
                    });
                }
                //Actualizar nuestra propiedad 
                vmDashBoard.SaleLastWeek = listSaleWeek;
            }
            catch {
                throw;
            }
            //Retornamos nuestro modelo
            return vmDashBoard;
        }


        //Metodo privado que solo sera para esta clase
        //Retornara una consulta de tipo venta "returnSales"
        //Su objetivo sera recibir la tabla de venta
        //Cantidad de dias que deseamos restar "deductAmountDays" 
        private IQueryable<Sale> returnSales(IQueryable<Sale> tableSale, int deductAmountDays) //Retornar ventas
        {
            //Devolver todo un rango de ventas dependiendo del la fecha que le indiquen, tomando en cuenta el dia en el que estas
            //Ultima fecha es igaul a tablaVenta
            DateTime? lastDate = tableSale.OrderByDescending(v => v.DateRegistration).Select(v => v.DateRegistration).First();
            lastDate = lastDate.Value.AddDays(deductAmountDays);//Restar cantidad de dias
            return tableSale.Where(v => v.DateRegistration.Value.Date >= lastDate.Value.Date);
        }

        //Metodo que solo devolvera un entero
        //Total de ventas de la ultima semana "SaleLastWeek"
        private async Task<int> SaleLastWeek()
        {
            int total = 0;
            IQueryable<Sale> _saleQuery = await _saleRepository.GetAllConsult();

            if (_saleQuery.Count() > 0)
            {
                var tableSale = returnSales(_saleQuery, -7);
                total = tableSale.Count();
            }
            return total;
        }

        //Metodo para el total de ingresos de toda la semana
        private async Task<string> TotalIncome()
        {

            decimal result = 0;
            IQueryable<Sale> _saleQuery = await _saleRepository.GetAllConsult();


            if (_saleQuery.Count() > 0)
            {
                var tableSale = returnSales(_saleQuery, -7);

                result = tableSale.Select(v => v.SaleTotal).Sum(v => v.Value); 
            }

            //Retornar nuestro resultado pero como es decimal hay que convertirlo a string 
            return Convert.ToString(result, new CultureInfo("es-MX"));


        }


        //Creamos otro metodo de totalProductos
        private async Task<int> TotalProducts()
        {
            IQueryable<Product> _productoQuery = await _productRepository.GetAllConsult();
            int total = _productoQuery.Count();
            return total;
        }

        //Creamos otro metodo de Diccionario en el cual Obtiene el número de ventas por día durante la última semana.
        //Usa returnSales para obtener las ventas de los últimos 7 días y agrupa las ventas por fecha.
        private async Task<Dictionary<string, int>> SalesLastWeek() //SaleLastWeek ventas de la ultima semana
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            IQueryable<Sale> _saleQuery = await _saleRepository.GetAllConsult();

            if (_saleQuery.Count() > 0)
            {
                var tableSale = returnSales(_saleQuery, -7);

                result = tableSale.
                    GroupBy( v=> v.DateRegistration.Value.Date).OrderBy(g => g.Key)
                    .Select(dv => new { date = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count()})
                    .ToDictionary(keySelector: r => r.date, elementSelector: r => r.total);
            }
            return result;
        }
    }
}

