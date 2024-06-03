using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public class MenuController : ControllerBase
    {



        //Creamos el servicio que vamos a utilizar  que seran de tipo lectura por eso el private
        private readonly IMenuService _menuService;

        //Creamos el constructor y lo instanciamos dentro de el
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }





        //Creamos todos los metodos que tendra el controllador

        //Metodo Get para acceder a toda la lista de menus
        [HttpGet]
        [Route("List")]//Ruta de como vamos a acceder a esta api
                       //Recibimos el int idUseer ya que vamos a obtener todos los menus que le pertenece
        public async Task<IActionResult> GetAllListMenu(int idUseer)
        {
            //Variable que recibe una respuesta de tipo objeto
            //Configuramos para que sea una lista de objetoDTO en este caso ProductDTO
            var response = new ResponseApi<List<MenuDTO>>();

            try
            {
                //Si la respuesta es correcta
                //Llammos a la variable response
                response.status = true;
                // Método "ListMenu" nos devuelve todos los menús
                response.value = await _menuService.ListMenu(idUseer);
            }
            catch (Exception exceptionError)
            {
                //Si la respuesta es falsa
                //En caso de que sea un error pasamos a la varible el tipo de valor falso
                // Y un mensaje de tipo error
                response.status = false;
                response.message = exceptionError.Message;
            }

            //Retornamos la respuesta
            return Ok(response);
        }
    }
}

