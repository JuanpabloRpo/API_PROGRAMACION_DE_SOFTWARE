using AutoMapper;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Migrations;
using API_PROGRAMACION_DE_SOFTWARE.Controllers;

namespace API_PROGRAMACION_DE_SOFTWARE.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDAO _userDAO;
        private readonly ILogger<UserController> _logger;

        public UserService(IUserDAO userDAO, ILogger<UserController> logger)
        {
            _userDAO = userDAO;
            _logger = logger;
        }

        public async Task<List<User>> ListUsers()
        {
            List<User> result = await _userDAO.ListUsers();


            return result;
        }

        public async Task<User> GetUser(int userId)
        {
            
            var user = await _userDAO.GetUser(userId);
            if (user != null)
            {
                _logger.LogInformation($"Usuario con ID: {userId} encontrado.");
            }
            else
            {
                _logger.LogWarning($"No se encontró el usuario con ID: {userId}.");
            }
            return user;
        }

        public async Task<Boolean> CreateUser(User user)
        {
            var resultado = await _userDAO.CreateUser(user);
            if (resultado)
            {
                _logger.LogInformation("Usuario creado de manera exitosa.");
                return true;
            }
            else
            {
                _logger.LogError("Error al crear el usuario.");
                return false;
            }
        }

        public async Task<Boolean> UpdateUser(User user)
        {
            var resultado = await _userDAO.UpdateUser(user);
            if (resultado)
            {
                _logger.LogInformation("Usuario actualizado de manera exitosa.");
                return true;
            }
            else
            {
                _logger.LogError($"Error al actualizar el usuario con ID: {user.Id}.");
                return false;
            }
        }

        public async Task<Boolean> DeleteUser(int userId)
        {
          
            var resultado = await _userDAO.DeleteUser(userId);
            if (resultado)
            {
                _logger.LogInformation($"Usuario con ID: {userId} eliminado de manera exitosa.");
                return true;
            }
            else
            {
                _logger.LogError($"Error al eliminar el usuario con ID: {userId}.");
                return false;
            }
        }
    }
}