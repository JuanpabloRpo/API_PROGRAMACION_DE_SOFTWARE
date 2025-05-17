using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Entities;

namespace API_PROGRAMACION_DE_SOFTWARE.Services
{
    public class LoginService: ILoginService
    {

        private readonly ILoginDAO _loginDAO;

        public LoginService(ILoginDAO loginDAO)
        {
            _loginDAO = loginDAO;
        }

        public async Task<User> check(string UserName, string Password)
        {
           
            return await _loginDAO.SearchUser(UserName, Password);
        }
    }

}
