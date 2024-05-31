using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMegStore.DTO
{
    public class ProductDTO
    {
        public int IdProduct { get; set; }
        public string? Name { get; set; }
        public int? IdCategory { get; set; }
        public string? DescriptionCategory { get; set; }
        public int? Stock { get; set; }
        public string? Price { get; set; } /*Desde la interfaz del DTO vamos a recibir el precio de manera de texto, pero cuando pase a la BD pasara de strin a decimal*/
        public int? Active { get; set; }  //Cabiamos de bool a int ya que true sera 1, y el false sera 0  
    }
}
