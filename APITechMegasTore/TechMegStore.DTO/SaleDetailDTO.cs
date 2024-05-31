using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMegStore.DTO
{
    public class SaleDetailDTO
    {
 
        public int? IdProduct { get; set; }
        public string? DescriptionProduct { get; set; } // Propiedad Agregado Manualmente
        public int? Amount { get; set; }

        public string? PriceText { get; set; } /*Desde la interfaz del DTO vamos a recibir el precio de manera de texto, pero cuando pase a la BD pasara de strin a decimal*/

        public string? TotalText { get; set; } /*Desde la interfaz del DTO vamos a recibir  de manera de texto, pero cuando pase a la BD pasara de strin a decimal*/
    }
}
