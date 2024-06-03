using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregamos referencias
using TechMegStore.DATAS.Repositories.Interfaces;
using TechMegStore.DATAS.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TechMegStore.DATAS.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class //Especifica que es una clase clase y que se podra trabajar con todos los modelos
    {
        //Declaramos los metodos implementados dentro de la interfaz IGenericRepository del cual hereda


        //Creamos una varible  de lectura hacia el contexto de la base de datos
        private readonly TechMegastoreDbContext _dbContext;

        //Creamos el constructor donde estamos recibiendo el contexto, y lo almacena en nuestra variable de arriba   private readonly DbContext _dbContext;
       

        public GenericRepository(TechMegastoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        //Implementamos todos lo metodos que provienen de la interfaz


        //Metodo  obtener  GetModel
        public async Task<TModel> GetModel(Expression<Func<TModel, bool>> filter)
        {
            //Iniciamos un capturados de errores por si llegan a existir
            try
            {
                //Devolvemos el modelo que sera consultado
                //El modelo sea especificado, si  en dado no lo encuentra lo devolvera en nulo
                TModel model = await _dbContext.Set<TModel>().FirstOrDefaultAsync(filter); //filter por si lo llegamos a buscar por id, nombre etc
                return model; //retornara un modelo 

            }
            catch
            {//Caso que no devolvera un error
                throw;
            }
        }

        //Metodo  crear
        public async Task<TModel> PostModel(TModel model)
        {
            //Iniciamos un capturados de errores por si llegan a existir
            try
            {
                //Instanciamos la base de datos y el modelo del cual estaremos trabajando en este caso generico, asi que sera el que el usuario indique
                _dbContext.Set<TModel>().Add(model);//Pasamos el modelo
                await _dbContext.SaveChangesAsync();   //Guardamos cambios de manera asyncrona
                return model; //Devovemos el modelo, ya que automaticamente se asignara el id del modelo, en el caso de que sea exitoso, si no mostrara un error
            }
            catch
            {//Caso que no devolvera un error
                throw;
            }

        }

        //Metodo editar
        public  async Task<bool> PutModel(TModel model)
        {
            //Iniciamos un capturados de errores por si llegan a existir
            try
            {
                //Instanciamos la base de datos y el modelo del cual estaremos trabajando 
                _dbContext.Set<TModel>().Update(model);
                await _dbContext.SaveChangesAsync();   //Guardamos cambios de manera asyncrona
                return true; //Devuelve un true porque es bool 

            }
            catch
            {
                //Caso que no devolvera un error
                throw;
            }
        }

        //Metodo  Eliminar
        public  async Task<bool> DeleteModel(TModel model)
        {
            //Iniciamos un capturados de errores por si llegan a existir
            try
            {
                //Instanciamos la base de datos y el modelo del cual estaremos trabajando 
                _dbContext.Set<TModel>().Remove(model);
                await _dbContext.SaveChangesAsync();   //Guardamos cambios de manera asyncrona
                return true; //Devuelve un true por

            }
            catch
            {//Caso que no devolvera un error
                throw;
            }
        }

        //Metodo consultar 
        public async Task<IQueryable<TModel>> GetAllConsult(Expression<Func<TModel, bool>> filter = null)
        {
            //Iniciamos un capturados de errores por si llegan a existir
            try
            {
                //Ejecutamos una consulta que sera preparada donde se imlemente
                //Quien lo llame sea quien lo ejecute

                //Si nuestro filtro es igual a nulo, entonces no se ha especificado ningun filtro y devolvemos el cual esta haciendo la consulta

                IQueryable<TModel> queryModel = filter == null ? _dbContext.Set<TModel>() : _dbContext.Set<TModel>().Where(filter);
                return queryModel;   //Retornamos elqueryModel y ejecutamos a quien lo a llamadado
            }
            catch
            {//Caso que no devolvera un error
                throw;
            }
        }

       
    }
}
