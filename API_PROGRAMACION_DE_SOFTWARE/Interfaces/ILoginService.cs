using Microsoft.AspNetCore.Mvc;

namespace API_PROGRAMACION_DE_SOFTWARE.Interfaces
{
    public interface ILoginService
    {
        public Task<Boolean> check (string UserName, string Password);
    }
}
