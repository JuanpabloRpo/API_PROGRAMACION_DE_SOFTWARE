using AutoMapper;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Entities;

namespace API_PROGRAMACION_DE_SOFTWARE.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDAO _userDAO;

        public UserService(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public async Task<List<User>> ListUsers()
        {
            List<User> result = await _userDAO.ListUsers();

            return result;
        }

        public async Task<User> GetUser(int id)
        {
            User result = await _userDAO.GetUser(id);
            return result;
        }

        public async Task<Boolean> CreateUser(User user)
        {
            return await _userDAO.CreateUser(user);
        }

        public async Task<Boolean> UpdateUser(User user)
        {
            return await _userDAO.UpdateUser(user);
        }

        public async Task<Boolean> DeleteUser(int id)
        {
            return await _userDAO.DeleteUser(id);
        }
    }
}