
using Microsoft.AspNetCore.Mvc;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API_PROGRAMACION_DE_SOFTWARE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        

        public UserController(IUserService userService)
        {
            _userService = userService;
            
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> ListUsers()
        {
            var users = await _userService.ListUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userService.GetUser(userId);
            return user != null ? Ok(user) : NotFound("No se encontro el usuario");
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> CreateUser(User user)
        {
            return await _userService.CreateUser(user) ? Ok("Usuario creado"): BadRequest("No se pudo guardar el usuario.");
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            return await _userService.UpdateUser(user) ? Ok(): BadRequest("No se pudo actualizar el usuario.");

        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            return await _userService.DeleteUser(userId) ? Ok("Se elimino el usuario"): BadRequest("No se pudo eliminar el usuario.");
        }
    }
}