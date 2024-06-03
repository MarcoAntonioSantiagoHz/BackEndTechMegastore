using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Referencias
using TechMegStore.BLL.Services.Interface;
using TechMegStore.DTO;
using TechMegStore.WebApi.ApisRequests; //Utilidad



namespace TechMegStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RoleController : ControllerBase
    {

        //Creamos el servicio que vamos a utilizar  que seran de tipo lectura por eso el private
        private readonly IRoleService _roleService;

        //Creamos el constructor y lo instanciamos dentro de el
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        //Creamos todos los metodos que tendra


        //Metodo Get
        [HttpGet]
        [Route("List")]//Ruta de como vamos a acceder a esta api
        public async Task<IActionResult> GetAllListRole()
        {
            //Variable que recibe una respuesta de tipo objeto
            //Configuramos para que sea una lista de objetoDTO en este caso RoleDTO
            var response = new ResponseApi<List<RoleDTO>>();

            try
            {
                //Si la respuesta es correcta
                //Llammos a la variable response
                response.status = true;
                response.value = await _roleService.ListRole(); //Metodo "ListRole"nos devulve todos los roles
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

