using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMegStore.DTO
{
    //Esta clase nos permitira guardar la sesion del usuario quien se ha logeado
    public class SesionDTO
    {
        //Agregamos manualmente algunas propiedades
        public int IdUser { get; set; }
        public string? CompleteName { get; set; }
        public string? Email { get; set; }
        public string? RoleDescription { get; set; }
    }
}
