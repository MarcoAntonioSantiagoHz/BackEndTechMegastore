using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMegStore.BLL.Services;





//Referencias
using TechMegStore.BLL.Services.Interface;
using TechMegStore.DTO;
using TechMegStore.WebApi.ApisRequests; //Utilidad

namespace TechMegStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {

        //Creamos el servicio que vamos a utilizar  que seran de tipo lectura por eso el private
        private readonly IDashBoardService _dashBoardService;

        //Creamos el constructor y lo instanciamos dentro de el
        public DashBoardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }


        //Creamos todos los metodos que tendra el controllador




        //Metodo Get "Overview" para acceder al resumen
        [HttpGet]
        [Route("Overview")]//Ruta de como vamos a acceder a esta api
        public async Task<IActionResult> Overview()
        {
            //Variable que recibe una respuesta de tipo objeto
            //Configuramos para que sea una lista de objetoDTO en este caso DashBoardDTO
            var response = new ResponseApi<DashBoardDTO>();

            try
            {
                //Si la respuesta es correcta
                //Llammos a la variable response
                response.status = true;
                //Metodo "ListProduct"nos devulve todos los productos
                response.value = await _dashBoardService.Overview();  //"Overview" METODO RESUMEN 
            }
            catch (Exception exceptionError)
            {
                //Si la respuesta es falsa
                //En caso de que sea un error pasamos a la varible el tipo de valor falso
                // Y un mensaje de tipo error
                response.status = false;
                response.message = exceptionError.Message;
            }

            //Retornamos la respues
            return Ok(response);
        }






    }
}
