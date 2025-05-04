using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;
using Dapper;
using API_PROGRAMACION_DE_SOFTWARE.Utilities;
using API_PROGRAMACION_DE_SOFTWARE.Controllers;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Queries;

namespace API_PROGRAMACION_DE_SOFTWARE.DAOs
{
    public class UserDAO : IUserDAO
    {
        private readonly SQLServerConfiguration _connectionString;
        private readonly ILogger<UserController> _logger;

        public UserDAO(IOptions<SQLServerConfiguration> connectionString, ILogger<UserController> logger)
        {
            _connectionString = connectionString.Value;
            _logger = logger;
        }

        protected SqlConnection Connection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public async Task<List<User>> ListUsers()
        {
            List<User> result = new List<User>();

            try
            {
                using var db = Connection();
                IEnumerable<User> lista = await db.QueryAsync<User>(UserQueries.listUsers, new { });
                _logger.LogInformation("Consulta exitosa a SQL Server");
                return lista.ToList(); ;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al leer la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server: {ex.Message}");
            }

            return result;
        }
        public async Task<User> GetUser(int id)
        {
            try
            {
                using var db = Connection();
                var user = await db.QueryFirstOrDefaultAsync<User>(UserQueries.getUser, new { Id = id });
                _logger.LogInformation($"Búsqueda exitosa del usuario con ID: {id} en SQL Server");

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar el usuario con ID: {id} en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server (buscar): {ex.Message}");
                return null;
            }
        }

        public async Task<Boolean> CreateUser(User user)
        {
            int result = 0;
            try
            {
                using var db = Connection();
                result = await db.ExecuteAsync(UserQueries.createUser, new
                {
                    user.Document,
                    user.FirstName,
                    user.LastName,
                    user.MiddleName,
                    user.Age,
                    user.Email,
                    user.UserName,
                    user.Password,
                    user.TypeUser
                });
                return result > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al insertar en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server: {ex.Message}");
            }
            return result > 0;
        }

        public async Task<Boolean> UpdateUser(User user)
        {
            int result = 0;

            try
            {
                using var db = Connection();
                result = await db.ExecuteAsync(UserQueries.updateUser, new
                {
                    user.Id,
                    user.Document,
                    user.FirstName,
                    user.LastName,
                    user.MiddleName,
                    user.Age,
                    user.Email,
                    user.UserName,
                    user.Password,
                    user.Arrears,
                    user.TypeUser,
                    user.Role,
                    user.IsActive
                });
                return result > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server: {ex.Message}");
            }
            return result > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            int result = 0;
            try
            {
                using var db = Connection();
                result = await db.ExecuteAsync(UserQueries.deleteUser, new { Id = id });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el usuario con ID: {id} de la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server (eliminar): {ex.Message}");
                return false;
            }
        }
    }
}
