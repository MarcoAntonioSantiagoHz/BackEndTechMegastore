using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Importacion de referencias
using TechMegStore.DTO;


namespace TechMegStore.BLL.Services.Interface
{
    public interface IRoleService
    {
        //Metodos de servicios de rol

        //Metodo que devuelve una lista de tipo DTO
        Task<List<RoleDTO>> ListRole();

        

    }
}
