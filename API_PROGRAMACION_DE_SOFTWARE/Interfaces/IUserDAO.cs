using API_PROGRAMACION_DE_SOFTWARE.Entities;

namespace API_PROGRAMACION_DE_SOFTWARE.Interfaces
{
    public interface IUserDAO
    {
        Task<List<User>> ListUsers();
        Task<User> GetUser(int id);
        Task<Boolean> CreateUser(User user);
        Task<Boolean> UpdateUser(User user);
        Task<Boolean> DeleteUser(int id);
    }
}
