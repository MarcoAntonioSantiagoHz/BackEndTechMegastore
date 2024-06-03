using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Referencias 
using TechMegStore.DTO;

namespace TechMegStore.BLL.Services.Interface
{
    public interface ISaleService
    {
        //METODOS QUE IRAN DE LA INTERFAZ AL SERVICE

        Task<SaleDTO> Register(SaleDTO model);


        //Historial de venta "SalesHistory", y lo buscaremos por "searchFor", numero de venta o por fechas
        //Numero de venta "numberSale"
        //Fecha de inicio "startDate", Fecha de fin "dateEnd" 
        Task<List<SaleDTO>> SalesHistory(string searchFor, string numberSale, string startDate, string dateEnd);

        //Devolvera un reporteDto y necesitaremos los parametros de fehca de inicio y de fin string startDate, string dateEnd
        Task<List<ReportDTO>> Report(string  startDate, string dateEnd);
    }
}
