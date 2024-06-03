using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Referencia a nuestra capa
using TechMegStore.Models;

namespace TechMegStore.DATAS.Repositories.Interfaces
{

    //Creamos la herencia para utilizar el IGenericRepository
    public interface ISaleRepository : IGenericRepository<Sale> //Estamos trabajando el modelo Sale(Venta)
    {
        //Creamos el metodo de Registrar una venta
        Task<Sale> Register(Sale model);
    }
}
