using Microsoft.EntityFrameworkCore;
using ProyectoColProfesionales.Models.DB1;

namespace ProyectoColProfesionales.Servicios.Contrato
{
    public interface IUserService
    {
        Task<User> GetUser(string username, string password);

    }
}
