using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace API_PROGRAMACION_DE_SOFTWARE.Services
{
    public class LoginService: ILoginService
    {

        private readonly ILoginDAO _loginDAO;

        public LoginService(ILoginDAO loginDAO)
        {
            _loginDAO = loginDAO;
        }

        public async Task<Boolean> check(string UserName, string Password)
        {
            return await _loginDAO.SearchUser(UserName, Password) != null? true:false;
        }
    }

}
