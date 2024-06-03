using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//REFERENCIAS
using AutoMapper;
//using TechMegStore.BLL.Services.Interfacess;
using TechMegStore.BLL.Services.Interface;
using TechMegStore.DATAS.Repositories.Interfaces;
using TechMegStore.DTO;
using TechMegStore.Models;

//CLASE QUE IMPLEMENTARA LA INTERFAZ IRoleService

namespace TechMegStore.BLL.Services
{
    public class RoleService : IRoleService //Implementamos interfaz
    {
        //Creamos dos variables privadas de tipo lectura
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        //Generamos constructor
        public RoleService(IGenericRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<RoleDTO>> ListRole()
        {
           try {
                var listRole = await _roleRepository.GetAllConsult(); //Trae un Queryable de tipo rol
                return _mapper.Map<List<RoleDTO>>(listRole.ToList()); //Retorne una conversion de nuestro modelo ROL al modelo  RolDTO  
            }
            catch {
                throw; //por si sale error
            }
        }
    }
}
