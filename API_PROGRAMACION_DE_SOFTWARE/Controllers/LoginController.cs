using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Expressions;

namespace API_PROGRAMACION_DE_SOFTWARE.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService; 
        private readonly ILogger<LoginController> _logger; 

        public LoginController(ILoginService loginService, ILogger<LoginController> logger) 
        {
            _loginService = loginService;
            _logger = logger;
        }

        [HttpGet]
        [Route("VerificarUsuario")]
        public async Task<IActionResult> Check(string userName, string password) 
        {
            var user = await _loginService.check(userName, password);
            return  user != null ? Ok(user): NotFound();
        }
    }
}
