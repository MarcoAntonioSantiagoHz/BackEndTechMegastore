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

        }

    }
}
