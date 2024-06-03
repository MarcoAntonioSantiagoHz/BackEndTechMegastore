using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Referencias 
using TechMegStore.DTO;


namespace TechMegStore.BLL.Services.Interface
{
    public interface IUseerService
    {
        //METODOS QUE IRAN DE LA INTERFAZ AL SERVICE
        Task<List<UseerDTO>> ListUseer();

        //Metodo para validar las credenciales del usuario 
        //Nota: parámetros Email y Password no provienen directamente de la clase SesionDTO.
        //La clase SesionDTO es utilizada para representar la información de la sesión del usuario
        //después de que se ha autenticado exitosamente.
        Task<SesionDTO> ValidateCredentials(string email, string password);

        //Metodo para crear usuario
        //Recibe un modelo DTO y lo crea
        Task<UseerDTO> CreateUseer(UseerDTO model);

        //Metodo editar usuario
        //Recibe un modelo DTO y lo edita
        Task<bool> EditUser(UseerDTO model);

        //Metodo de eliminar 
        Task<bool> DeleteUser(int id);

    }
}
