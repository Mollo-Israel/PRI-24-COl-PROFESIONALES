using Microsoft.EntityFrameworkCore;
using ProyectoColProfesionales.Data;
using ProyectoColProfesionales.Models.DB1;
using ProyectoColProfesionales.Servicios.Contrato;

namespace ProyectoColProfesionales.Servicios.Implementacion
{
    public class UserService : IUserService
    {
        private readonly DBColProfessionalContext _dbContext;
        public UserService(DBColProfessionalContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<User> GetUser(string username, string password)
        {
            User user_encontrado = await _dbContext.Users.Where(u => u.UserName == username && u.Password == password)
                    .FirstOrDefaultAsync();
            return user_encontrado;

        }

    }
}
