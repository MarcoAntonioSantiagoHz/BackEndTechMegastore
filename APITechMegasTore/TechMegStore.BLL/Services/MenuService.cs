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
{  //Clase  que crea Herencia de la interfaz IProduct
    public class MenuService : IMenuService
    {




        //Creamos dos variables privadas de tipo lectura
        private readonly IGenericRepository<Useer> _useertRepository;
        private readonly IGenericRepository<MenuRole> _menuRoleRepository;
        private readonly IGenericRepository<Menu> _menupository;
        private readonly IMapper _mapper;

        //Generamos constructor, instanciando parametros
        public MenuService(IGenericRepository<Useer> useertRepository,
            IGenericRepository<MenuRole> menuRoleRepository,
            IGenericRepository<Menu> menuepository, IMapper mapper)
        {
            _useertRepository = useertRepository;
            _menuRoleRepository = menuRoleRepository;
            _menupository = menuepository;
            _mapper = mapper;
        }


        //Creamos los metodos
        public async Task<List<MenuDTO>> ListMenu(int idUser)
        {
            //Creamos el queryable que instanciara a la BD
            //"tableUseer", solo es una variable para hacer referencia a la tabla usuario
            IQueryable<Useer> tableUseer = await _useertRepository.GetAllConsult(useer => useer.IdUser == idUser);

            //"tableMenuRole", solo es una variable para hacer referencia a la tabla de menuRol
            IQueryable<MenuRole> tableMenuRole = await _menuRoleRepository.GetAllConsult();
            //"tableMenu", solo es una variable para hacer referencia a la tabla de tableMenu
            IQueryable<Menu> tableMenu = await _menupository.GetAllConsult();


            //Logica para obtener los menus
            try
            {
                IQueryable<Menu> tableResult = (from u in tableUseer
                                                join mr in tableMenuRole on u.IdRole equals mr.IdRole
                                                join m in tableMenu on mr.IdMenu equals m.IdMenu
                                                select m).AsQueryable();
                ////ListaMenus                            
                var listsMenu = tableResult.ToList();
                return _mapper.Map<List<MenuDTO>>(listsMenu); //ListaMenus
            }
            catch {
                throw;
            }
        }
    }
}
