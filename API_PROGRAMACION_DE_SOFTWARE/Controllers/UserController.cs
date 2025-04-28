
using Microsoft.AspNetCore.Mvc;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;

namespace API_PROGRAMACION_DE_SOFTWARE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
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
            if (user != null)
            {
                _logger.LogInformation($"Usuario con ID: {userId} encontrado.");
                return Ok(user);
            }
            else
            {
                _logger.LogWarning($"No se encontró el usuario con ID: {userId}.");
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> CreateUser(User user)
        {
            bool resultado = await _userService.CreateUser(user);
            if (resultado)
            {
                _logger.LogInformation("Usuario creado de manera exitosa.");
                return Ok();
            }
            else
            {
                _logger.LogError("Error al crear el usuario.");
                return BadRequest("No se pudo guardar el usuario.");
            }
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            bool resultado = await _userService.UpdateUser(user);
            if (resultado)
            {
                _logger.LogInformation("Usuario actualizado de manera exitosa.");
                return Ok();
            }
            else
            {
                _logger.LogError($"Error al actualizar el usuario con ID: {user.Id}.");
                return BadRequest("No se pudo actualizar el curso.");
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            bool resultado = await _userService.DeleteUser(userId);
            if (resultado)
            {
                _logger.LogInformation($"Usuario con ID: {userId} eliminado de manera exitosa.");
                return NoContent();
            }
            else
            {
                _logger.LogError($"Error al eliminar el usuario con ID: {userId}.");
                return NotFound();
            }
        }
    }
}