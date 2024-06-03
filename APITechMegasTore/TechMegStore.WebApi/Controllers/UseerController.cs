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
    public class UseerController : ControllerBase
    {

        //Creamos el servicio que vamos a utilizar  que seran de tipo lectura por eso el private
        private readonly IUseerService _useerService;

        //Creamos el constructor y lo instanciamos dentro de el
        public UseerController(IUseerService useerService)
        {
            _useerService = useerService;
        }


        //Creamos los metodos


        //Metodo Get para acceder a toda la lista de usuarios
        [HttpGet]
        [Route("List")]//Ruta de como vamos a acceder a esta api
        public async Task<IActionResult> GetAllListUseer()
        {
            //Variable que recibe una respuesta de tipo objeto
            //Configuramos para que sea una lista de objetoDTO en este caso UseerDTO
            var response = new ResponseApi<List<UseerDTO>>();

            try
            {
                //Si la respuesta es correcta
                //Llammos a la variable response
                response.status = true;
                response.value = await _useerService.ListUseer(); //Metodo "ListUseer"nos devulve todos los usuarios
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


        //"Metodo Para "VALIDAR" validar un usuario por medio de enviar/Crear "
        [HttpPost]
            [Route("log_in")]//Ruta de como vamos a acceder a esta api en este caso con IniciarSesion
        public async Task<IActionResult> LogIn([FromBody] LoginDTO login)
            {
                //Variable que recibe una respuesta de tipo objeto
                //Configuramos para que sea una lista de objetoDTO en este caso sessionDto
                var response = new ResponseApi<SesionDTO>();

                try
                {
                    //Si la respuesta es correcta
                    //Llammos a la variable response
                    response.status = true;
                    response.value = await _useerService.ValidateCredentials(login.Email, login.Password);
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


        //Metodo para crear y guardar un usuario
        [HttpPost]
        [Route("SaveUser")]//Ruta de como vamos a acceder a esta api  solo "Guardar"
        public async Task<IActionResult> CreateUseer([FromBody] UseerDTO user)
        {
            //Variable que recibe una respuesta de tipo objeto
            //Configuramos para que sea una lista de objetoDTO en este caso UseerDTO
            var response = new ResponseApi<UseerDTO>();

            try
            {
                //Si la respuesta es correcta
                //Llammos a la variable response
                response.status = true;
                //Pasamos o instanciamos el metodo a utilizar
                response.value = await _useerService.CreateUseer(user);
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


        //Metodo para Editar
        [HttpPut]
        [Route("EditUser")]//Ruta de como vamos a acceder a esta api  solo "Guardar"
        public async Task<IActionResult> EditUseer([FromBody] UseerDTO user)
        {
            //Variable que recibe una respuesta de tipo objeto
            //Configuramos para que sea una lista de objetoDTO en este caso UseerDTO
            var response = new ResponseApi<bool>();

            try
            {
                //Si la respuesta es correcta
                //Llammos a la variable response
                response.status = true;
                //Pasamos o instanciamos el metodo a utilizar
                response.value = await _useerService.EditUser(user);
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



        //Metodo para poder Eliminar 
        [HttpDelete]
        [Route("DeleteUser/{id:int}")]//Ruta de como vamos a acceder a esta api  eliminar por id
        public async Task<IActionResult> DeleteUseer(int id)
        {
            //Variable que recibe una respuesta de tipo objeto
            //Configuramos para que sea una lista de objetoDTO en este caso UseerDTO
            var response = new ResponseApi<bool>();

            try
            {
                //Si la respuesta es correcta
                //Llammos a la variable response
                response.status = true;
                //Pasamos o instanciamos el metodo a utilizar
                response.value = await _useerService.DeleteUser(id);
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

