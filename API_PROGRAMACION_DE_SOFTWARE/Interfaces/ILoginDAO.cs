using API_PROGRAMACION_DE_SOFTWARE.Entities;

namespace API_PROGRAMACION_DE_SOFTWARE.Interfaces
{
    public interface ILoginDAO
    {
        public Task<User> SearchUser(string UserName, string Password);
        
    }
}