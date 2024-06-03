using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Linq.Expressions;

namespace TechMegStore.DATAS.Repositories.Interfaces
{
    //Crearemos todos los metodos de los cuales vamos a trabajar directamente con nuestros modelos en general
    public interface IGenericRepository<TModel> where TModel : class //Especifica que es una clase clase y que se podra trabajar con todos los modelos
    {
        //Creamos metodos para GET,POST, PUT, DELETE
        //Se trabajara de manera asyncrona utilizando el Task


        //1.er Metodo --"OBTENER"--, se encargara de devolver algun modelo  ya sea un menu, una categoria, un usuario, un rol etc.
        //GetModel: Recupera un único registro, retorna un TModel.
        Task<TModel> GetModel(Expression<Func<TModel, bool>> filter); //Este parametro se llama filter

        //2.do Metodo crear,Recibimos un model para poder crear  ya sea un menu, una categoria, un usuario, un rol etc.
        Task<TModel> PostModel(TModel model);

        //3.er Metodo Editar, 

        Task<bool> PutModel(TModel model);


        //4.to Modelo Eliminar
        Task<bool> DeleteModel(TModel model);

        //5.to Esto devolvera la consulta del modelo,
        //este método --"CONSULTAR"-- facilita la recuperación de datos de una manera flexible,
        //permitiendo aplicar condiciones de filtrado cuando sea necesario.
        //GetAll: Recupera múltiples registros, retorna un IQueryable<TModel>.
        Task<IQueryable<TModel>> GetAllConsult(Expression<Func<TModel, bool>> filter = null); //Este parametro se llama filter



    }
}

