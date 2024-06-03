using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Referencias 
using TechMegStore.DTO;
namespace TechMegStore.BLL.Services.Interface
{
    public interface IMenuService
    {

        //Retornara una lista de menuDto
        //Es de tipo entero porque recibiremos el id del usuario
        Task<List<MenuDTO>> ListMenu(int idUseer);  
    }
}
