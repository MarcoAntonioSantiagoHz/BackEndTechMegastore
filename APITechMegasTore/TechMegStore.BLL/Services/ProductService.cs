using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Referencias
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechMegStore.BLL.Services.Interface;
using TechMegStore.DATAS.Repositories.Interfaces;
using TechMegStore.DTO;
using TechMegStore.Models;



namespace TechMegStore.BLL.Services
{
    //Clase  que crea Herencia de la interfaz IProduct
    public class ProductService : IProductService
    {
        //Clase que implementara los metodos de la interfaz IProductService

        //Creamos dos variables privadas de tipo lectura
        private readonly IGenericRepository<Product> _productRepository; //Cambia nombre de variable _productRepository
        private readonly IMapper _mapper;

        //Generamos constructor, instanciando parametros
        public ProductService(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        //Creamos metodos
        public async Task<List<ProductDTO>> ListProduct()
        {
            try
            {
                // Llamamos al _productRepository y accedemos a Consultar
                var queryProduct = await _productRepository.GetAllConsult();
                //Esta nos devuelve un IQueryable de productos pero incluimos categoria => "cat"
                var listProducts = queryProduct.Include(cat => cat.IdCategoryNavigation).ToList();

                //Vamos a convertir esta lista de tipo usuarioDTO
                //Entre parentesis el origen
                return _mapper.Map<List<ProductDTO>>(listProducts.ToList());
            }
            catch
            {
                throw;
            }
        }


        public async Task<ProductDTO> CreateProduct(ProductDTO model)
        {
            try
            {
                //Varible productoCreado "createdProduct"
                //Como es un DTO lo convertimos ya que necesita el mapper para pasar a producto

                var createdProduct = await _productRepository.PostModel(_mapper.Map<Product>(model));


                //Validar si el producto a sido creado

                //Creamos una condicional que va a acceder y si es igual a cero no puedo crearlo 
                if (createdProduct.IdProduct == 0)
                {
                    //Devuelve el error con una exepcion
                    throw new TaskCanceledException("Error! could not create product"); //producto no creado
                }

                //Devolvemos ese producto que ha sido creado
                //<ProductDTO> la clase a la cual deseamos convertir
                //(createdProduct); la clase a convertir
                return _mapper.Map<ProductDTO>(createdProduct);
            }
            catch
            {
                throw;
            }
        }

        public async  Task<bool> EditProduct(ProductDTO model)
        {
            try
            {
                //Creamos una varibale para obtener el producto en forma de modelo,
                //ya que lo estamos recibiendo de forma DTO y eso no queremos, para ello necesitamos el Mapper
                var productModel = _mapper.Map<Product>(model);
                //Variable de producto encontrado, con el "Obtener"
                var productFound = await _productRepository.GetModel(productEdit =>
                   //Filtros para obtener el producto por su idProducto
                   //variable productEdit
                   productEdit.IdProduct == productModel.IdProduct
                );

                //Validamos el producto encontrado "productFound" sea nulo 
                if (productFound == null)
                {
                    //Devuelve el error con una exepcion
                    throw new TaskCanceledException("Error! producto no exists"); //producto no existe
                }

                //Ahora actualizamos cada una de nuestras propiedades de nuestro producto
                //En caso de que el producto si existe, y necesitamos editar sus propiedades
                productFound.Name = productModel.Name;
                productFound.IdCategory = productModel.IdCategory;

                productFound.Stock = productModel.Stock;
                productFound.Price = productModel.Price;
                productFound.Active = productModel.Active;

               
                //Ahora enviamos todo esto hacia el metodo de poder editar y obtener respuesta
                //Variable de tipo respuesta
                bool response = await _productRepository.PutModel(productFound);

                //Validar la respuesta
                //Si es falso, lo convertirmos a true
                if (!response)
                {
                    throw new TaskCanceledException("Can Not Be Edited"); //No se puede editar
                }
                return response; //Devolvemos la respuesta

            }
            catch
            {
                throw;
            }
        }


        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                //Variable de tipo producto encontrado
                //GetModel obtener
                var productFound = await _productRepository.GetModel(productDelete => productDelete.IdProduct == id);

                //Validar si en verdad a enontrado un producto
                if (productFound == null)//Si es igual a nulo quiere decir que no lo ha encontrado
                {
                    //Devuelve el error con una exepcion
                    throw new TaskCanceledException("Product No Exist"); //El producto no existe
                }

                //Variable para recibir el resultado de nuestro metodo
                bool response = await _productRepository.DeleteModel(productFound);

                //Validar si no se pudo eliminar 
                if (!response)
                {
                    throw new TaskCanceledException("Can Not Be Delete"); //No se puede eliminar
                }
                return response; //Devolvemos la respuesta
            }
            catch
            {
                throw;
            }
        }




    }
}
