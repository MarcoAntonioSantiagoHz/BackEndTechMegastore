using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMegStore.DTO
{
    //Esta clase nos permitira recibir las credenciales del usuario para poder acceder al sistema
    public class LoginDTO
    {
        //Agregamos manualmente algunas propiedades
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
