using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Referencias
using AutoMapper;
using TechMegStore.BLL.Services.Interface;
using TechMegStore.DATAS.Repositories.Interfaces;
using TechMegStore.DTO;
using TechMegStore.Models;

namespace TechMegStore.BLL.Services
{ //Clase  que crea Herencia de la interfaz ICategoryService
    public class CategoryService : ICategoryService
    {
        //Clase que implementara los metodos de la interfaz ICategoryService

        //Creamos dos variables privadas de tipo lectura
        private readonly IGenericRepository<Category> _categoryRepository; //Cambia nombre de variable _categoryRepository
        private readonly IMapper _mapper;

        //Generamos constructor, instanciando parametros
        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        //Metodo que proviene de la interfaz
        public async Task<List<CategoryDTO>> ListCategory()
        {
            try
            {
                //GetAllConsult Devuelve una consulta
                var listCategory = await _categoryRepository.GetAllConsult(); //Devuelve un IQueryable
                return _mapper.Map<List<CategoryDTO>>(listCategory.ToList()); //Retorne una conversion de nues
            } 
            catch
            {
                throw;
            }
        }
    }
}
