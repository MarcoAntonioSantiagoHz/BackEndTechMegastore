using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Referencias 
using TechMegStore.DTO;

namespace TechMegStore.BLL.Services.Interface
{
    public interface IDashBoardService
    {
        //METODOS DE LA INTERFAZ A SERVICE

        //Este metodo lo unico que hara es un resumen "Overview"
        Task<DashBoardDTO> Overview(); //Resumen
    }
}
