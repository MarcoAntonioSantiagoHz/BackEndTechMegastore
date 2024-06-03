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
    public class CategoryController : ControllerBase
    {

        //Creamos el servicio que vamos a utilizar  que seran de tipo lectura por eso el private
        private readonly ICategoryService _categoryService;

        //Creamos el constructor y lo instanciamos dentro de el
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        //Creamos metodos


        //Metodo Get para acceder a toda la lista de categorias
        [HttpGet]
        [Route("List")]//Ruta de como vamos a acceder a esta api
        public async Task<IActionResult> GetAllListCategory()
        {
            //Variable que recibe una respuesta de tipo objeto
            //Configuramos para que sea una lista de objetoDTO en este caso CategoryDTO
            var response = new ResponseApi<List<CategoryDTO>>();

            try
            {
                //Si la respuesta es correcta
                //Llammos a la variable response
                response.status = true;
                //Metodo "ListCategory"nos devulve todos las categorias
                response.value = await _categoryService.ListCategory(); 
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
