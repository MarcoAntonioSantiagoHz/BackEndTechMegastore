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
    public class ProductController : ControllerBase
    {

        //Creamos el servicio que vamos a utilizar  que seran de tipo lectura por eso el private
        private readonly IProductService _productService;

        //Creamos el constructor y lo instanciamos dentro de el
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        //Creamos todos los metodos que tendra


        //Metodo Get para acceder a toda la lista de productos
        [HttpGet]
        [Route("List")]//Ruta de como vamos a acceder a esta api
        public async Task<IActionResult> GetAllListProduct()
        {
            //Variable que recibe una respuesta de tipo objeto
            //Configuramos para que sea una lista de objetoDTO en este caso ProductDTO
            var response = new ResponseApi<List<ProductDTO>>();

            try
            {
                //Si la respuesta es correcta
                //Llammos a la variable response
                response.status = true;
                //Metodo "ListProduct"nos devulve todos los productos
                response.value = await _productService.ListProduct(); 
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



        //Metodo para crear y guardar un producto
        [HttpPost]
        [Route("SaveProduct")]//Ruta de como vamos a acceder a esta api  solo "Guardar"
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO product)
        {
            //Variable que recibe una respuesta de tipo objeto
            //Configuramos para que sea una lista de objetoDTO en este caso ProductDTO
            var response = new ResponseApi<ProductDTO>();

            try
            {
                //Si la respuesta es correcta
                //Llammos a la variable response
                response.status = true;
                //Pasamos o instanciamos el metodo a utilizar
                response.value = await _productService.CreateProduct(product);
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
        [Route("EditProduct")]//Ruta de como vamos a acceder a esta api  solo "Guardar"
        public async Task<IActionResult> EditProduct([FromBody] ProductDTO product)
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
                response.value = await _productService.EditProduct(product);
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
        [Route("DeleteProduct/{id:int}")]//Ruta de como vamos a acceder a esta api  eliminar por id
        public async Task<IActionResult> DeleteProduct(int id)
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
                response.value = await _productService.DeleteProduct(id);
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
