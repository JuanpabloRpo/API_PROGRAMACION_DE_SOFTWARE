using API_PROGRAMACION_DE_SOFTWARE.Controllers;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Queries;
using API_PROGRAMACION_DE_SOFTWARE.Utilities;
using API_PROGRAMACION_DE_SOFTWARE.Enumerations;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;


namespace API_PROGRAMACION_DE_SOFTWARE.DAOs
{
    public class LoginDAO : ILoginDAO
    {
        private readonly SQLServerConfiguration _connectionString;
        private readonly ILogger<LoanController> _logger;


        public LoginDAO(IOptions<SQLServerConfiguration> connectionString, ILogger<LoanController> logger)
        {
            _connectionString = connectionString.Value;
            _logger = logger;
        }

        protected SqlConnection Connection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public async Task<User> SearchUser(string userName, string password)
        {
            try
            {
                using var db = Connection();
                var result = await db.QueryFirstOrDefaultAsync<User>(LoginQueries.SearchUser, new { UserName = userName, Password = password });
                _logger.LogInformation("Consulta exitosa de usuario en SQL Server");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error, no se a podido verificar los datos del usuario: {ex.Message}");
                Console.WriteLine($"Error SQL Server: {ex.Message}");
            }
            return null;
        }

    }
}

