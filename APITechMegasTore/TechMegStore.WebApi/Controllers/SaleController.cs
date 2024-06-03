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
    public class SaleController : ControllerBase
    {

        //Creamos el servicio que vamos a utilizar  que seran de tipo lectura por eso el private
        private readonly ISaleService _saleService;

        //Creamos el constructor y lo instanciamos dentro de el
        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }


        //Creamos todos los metodos que tendra



        //Metodo para crear y guardar un Registro
        [HttpPost]
        [Route("Register")]//Ruta de como vamos a acceder a esta api  solo "Guardar"
        public async Task<IActionResult> CreateRegister([FromBody] SaleDTO sale)
        {
            //Variable que recibe una respuesta de tipo objeto
            //Configuramos para que sea una lista de objetoDTO en este caso SaleDTO
            var response = new ResponseApi<SaleDTO>();

            try
            {
                //Si la respuesta es correcta
                //Llammos a la variable response
                response.status = true;
                //Pasamos o instanciamos el metodo a utilizar
                response.value = await _saleService.Register(sale);
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








        //Metodo Get para acceder al "Historial" de toda las ventas
        [HttpGet]
        [Route("History")]//Ruta de como vamos a acceder a esta api
        public async Task<IActionResult> History(string searchFor, string? numberSale, string? startDate, string? dateEnd)
        {
            //Variable que recibe una respuesta de tipo objeto
            //Configuramos para que sea una lista de objetoDTO en este caso SaleDTO
            var response = new ResponseApi<List<SaleDTO>>();
            numberSale = numberSale is null ? "" : numberSale;
            startDate = startDate is null ? "" : startDate;
            startDate = dateEnd is null ? "" : dateEnd;


            try
            {
                //Si la respuesta es correcta
                //Llammos a la variable response
                response.status = true;
                response.value = await _saleService.SalesHistory(searchFor, numberSale, startDate, dateEnd);
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




        //Metodo Get "Report" para acceder todos los reportes
        //Report(string startDate, string dateEnd);
        [HttpGet]
        [Route("Report")]//Ruta de como vamos a acceder a esta api
        public async Task<IActionResult> Report(string? startDate, string? dateEnd)
        {
            //Variable que recibe una respuesta de tipo objeto
            //Configuramos para que sea una lista de objetoDTO en este caso ReportDTO
            var response = new ResponseApi<List<ReportDTO>>();
           
            try
            {
                //Si la respuesta es correcta
                //Llammos a la variable response
                response.status = true;
                response.value = await _saleService.Report(startDate, dateEnd);
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
