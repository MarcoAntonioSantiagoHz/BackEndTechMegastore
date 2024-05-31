using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMegStore.DTO
{
    public class ReportDTO
    {
        // Propiedades Agregado Manualmente
        public string? DocumentNumber { get; set; } //numero de documento
        public string? PaymentType { get; set; } //tipo de pagp
        public string? DateRegistration { get; set; } //fecha de registro
        public string? SaleTotal { get; set; } //Total venta
        public string? Product { get; set; }
        public int? Amount { get; set; } //Cantidad
        public string? Price { get; set; }
        public string? Total { get; set; }


    }
}
