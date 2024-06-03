using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Referencias 
using TechMegStore.DTO;


namespace TechMegStore.BLL.Services.Interface
{
    public interface ICategoryService
    {
        //Unico metodo devolvera una lista de cateroriasDto
        Task<List<CategoryDTO>> ListCategory(); 
    }
}
