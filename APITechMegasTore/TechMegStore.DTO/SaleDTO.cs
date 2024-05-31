using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMegStore.DTO
{
    public class SaleDTO
    {
        public int IdSale { get; set; }

        public string? DocumentNumber { get; set; } //Numero de documento

        public string? PaymentType { get; set; }

        public string? SaleTotalText { get; set; }/*Desde la interfaz del DTO vamos a recibir  de manera de texto, pero cuando pase a la BD pasara de strin a decimal*/

        public string? DateRegistration { get; set; } //Fecha de registro Desde la interfaz del DTO vamos a recibir  de manera de texto, pero cuando pase a la BD pasara de string a dateTime*/
     
        public virtual ICollection<SaleDetailDTO> SaleDetails { get; set; } 
    }
}
