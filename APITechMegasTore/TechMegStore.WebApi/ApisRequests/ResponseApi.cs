
namespace TechMegStore.WebApi.ApisRequests
{
    //Esta clase servira como respuesta de todas nuestras solicitudes  de nuestras APIS

    //La volvemos generica, la cual podremos recibir cualquier objeto
    public class ResponseApi<T>
    {
        //Creamos 3 propiedades
        //Retorna si la operacion a sido exitosa
        public bool status { get; set; }

        //Devuele el objeto por eso la letra "T" ya que es lo que estamos recibiendo
        public T  value { get; set; }

        //Devuelve un mensaje
        public string message { get; set; }

    }
}
