using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Referencias 
using TechMegStore.DTO;

namespace TechMegStore.BLL.Services.Interface
{
    public interface IProductService
    {
        //METODOS DE LA INTERFAZ A SERVICE
        Task<List<ProductDTO>> ListProduct();

        //Metodo para crear productos
        //Recibe un modelo DTO y lo crea
        Task<ProductDTO> CreateProduct(ProductDTO model);

        //Metodo editar producto
        //Recibe un modelo DTO y lo edita
        Task<bool> EditProduct(ProductDTO model);

        //Metodo de eliminar 
        Task<bool> DeleteProduct(int id);

    }
}
