using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMegStore.DTO
{
    public class DashBoardDTO
    {
        //PROPIEDADES AGREGADAS MANUALMENTE
        public int? SaleTotal { get; set; } //Total de ventas
        //public int? TotalIncome { get; set; } //CAMBIO Total ingresos
        public string? TotalIncome { get; set; } //Total ingresos
        public int? TotalProducts { get; set; }//Total de productos

        //Creamos una propiedad de tipo lista
        public List<SaleWeekDTO> SaleLastWeek { get; set; } //Ventas de la ultima semana
    }
}
    