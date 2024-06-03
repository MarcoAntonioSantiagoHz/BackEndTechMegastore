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
    //Clase  que crea Herencia de la interfaz IUseerService
    public class UseerService : IUseerService
    {
        //Clase que implementara los metodos de la interfaz IUseerService

        //Creamos dos variables privadas de tipo lectura
        private readonly IGenericRepository<Useer> _useerRepository; //Cambia nombre de variable _useerRepository
        private readonly IMapper _mapper;

        //Generamos constructor, instanciando parametros
        public UseerService(IGenericRepository<Useer> useerRepository, IMapper mapper)
        {
            _useerRepository = useerRepository;
            _mapper = mapper;
        }

        public async Task<List<UseerDTO>> ListUseer()
        {
            //
            try
            {//Llamamos al usuarioRepositorio y accedemos a Consultar
                var queryUseer = await _useerRepository.GetAllConsult();
                //Esta nos devuelve un IQueryable de usuarios con el rol de cada usuario
                var listUseers = queryUseer.Include(role => role.IdRoleNavigation).ToList();

                //Vamos a convertir esta lista de tipo usuarioDTO
                //Entre parentesis el origen
                return _mapper.Map<List<UseerDTO>>(listUseers);

            }
            catch
            {
                //En caso de error mandara un mensaje
                throw;
            }
        }

        public async Task<SesionDTO> ValidateCredentials(string email, string password)
        {
            try
            {
                //creamos la variable _useerRepository que llama a consular
                var queryUseer = await _useerRepository.GetAllConsult(useerSesion =>
                    //Ponemos la busqueda por medio de un filtro
                    useerSesion.Email == email &&
                    useerSesion.Password == password
                    );
                //Sentencia de control de la query "EN CASO DE QUE NO EXISTA EL USUARIO"

                //Solo quiero que devuelva el primero, Caso contrario que no exista o no lo encuentre devolvera un nulo
                if (queryUseer.FirstOrDefault() == null)
                {
                    //Devuelve el error con una exepcion
                    throw new TaskCanceledException("Im Sorry, Your User No Exist"); //Usuario no existe
                }

                //Variable que servira como respuesta de devolucion de usuario
                var returnUseer = queryUseer.Include(role => role.IdRoleNavigation).First();

                //Devolvemos el usuario pero no de "returnUseer", si no de "SesionDTO"
                return _mapper.Map<SesionDTO>(returnUseer);

            }
            catch
            {
                throw;

            }
        }

        //Metodo para crear un usuario
        public async Task<UseerDTO> CreateUseer(UseerDTO model)
        {
            try
            {

                //Varible usuarioCreado "createdUser"
                //CreateUseer(model); lo convertimos ya que necesita el mapper para pasar a usuario
                var createdUser = await _useerRepository.PostModel(_mapper.Map<Useer>(model));

                //Creamos una condicional que va a acceder y si es igual a cero no puedo crearlo 
                if (createdUser.IdUser == 0)
                {
                    //Devuelve el error con una exepcion
                    throw new TaskCanceledException("Error! could not create user"); //Usuario no creado
                }

                //Si No es igual a cero continuara
                //Creamos una variable que obtendra nuestro usuario atraves de nuestro IdUser
                var query = await _useerRepository.GetAllConsult(userCreat => userCreat.IdUser == userCreat.IdUser);

                //Actualizar el usuario creeado con la descripcion de su rol

                createdUser = query.Include(role => role.IdRoleNavigation).First();

                //Retornara el usuario, peroantes necesitara el mapper 
                //Para poder convertir en un Usuario a un UsuarioDto

                return _mapper.Map<UseerDTO>(createdUser); //UsuarioCreado 

            }
            catch
            {
                throw;
            }
        }


        public async Task<bool> EditUser(UseerDTO model)
        {
            try
            {
                //accedesmoos en la variable con mapper ya que necesitamos convertir nuestro modelo 
                //A un tipo de usuario pero que pertenezca a la clase de nuestros modelos
                var useerModel = _mapper.Map<Useer>(model);//Nuestro usuaroDto lo hemos convetido a solo Usuario

                //Logica
                //variable de usuarioEncontrado para
                //Verificar si el id de ese usuario existe en nuestra BD
                //GetModel obtener

                var useerFound = await _useerRepository.GetModel(
                    //Filtro por el idUsuario
                    useerEdit => useerEdit.IdUser == useerModel.IdUser);

                if (useerFound == null)
                {
                    throw new TaskCanceledException("User No Exists"); //El usuario no existe
                }

                //En caso de que el usuario si existe, y necesitamos editar sus propiedades
                useerFound.CompleteName = useerModel.CompleteName;
                useerFound.Email = useerModel.Email;
                useerFound.IdRole = useerModel.IdRole;
                useerFound.Password = useerModel.Password;
                useerFound.Active = useerModel.Active;

                //Ahora enviamos todo esto hacia el metodo de poder editar
                //Variable de tipo respuesta
                bool response = await _useerRepository.PutModel(useerFound);

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
                //Variable de tipo usuario encontrado
                //GetModel obtener
                var useerFound = await _useerRepository.GetModel(useerDelete => useerDelete.IdUser == id);

                //Validar si en verdad a enontrado un usuario
                if (useerFound == null)//Si es igual a nulo quiere decir que no lo ha encontrado
                {
                    //Devuelve el error con una exepcion
                    throw new TaskCanceledException("User No Exist"); //El Usuario no existe
                }

                //Variable para recibir el resultado de nuestro metodo
                bool response = await _useerRepository.DeleteModel(useerFound);

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
