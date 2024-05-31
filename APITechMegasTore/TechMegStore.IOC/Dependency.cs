using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMegStore.DATAS.DBContext;


//Utilizar referencias

using TechMegStore.DATAS.Repositories.Interfaces;
using TechMegStore.DATAS.Repositories;


//Hacemos referencias a nuestra capa utility de mapeos
using TechMegStore.Utility;

namespace TechMegStore.IOC
{
    //DEPENDENCIA HACIA NUESTRA BASE DE DATOS
    public  static class Dependency
    {

        //Creamos metodo que se encargara de recibir un servicio de colecciones,  IServiceCollection esto le pertenece a asp.netcore IServiceCollection
        //Dentro del servicio IServiceCollection se agregue el metodo InjectDependency
        public static void InjectDependency(this IServiceCollection services, IConfiguration configuration) {

            services.AddDbContext<TechMegastoreDbContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("stringSQL")); //Nombre de la cadena de conexion
            });



            //Creamos las dependencias  de los repositorios que hemos creado en este caso para modelo generico
            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            //Creamos la dependencia del  repositorio especificamente para las ventas 
            services.AddScoped<ISaleRepository, SaleRepository>();


            //Agregamos las dependencias de Automapper con todos los Mappeos
            services.AddAutoMapper(typeof(AutoMapperProfile)); //El AutoMapperProfile es Donde se encuentran todos nuestros mapeos 
        }

    }
}
