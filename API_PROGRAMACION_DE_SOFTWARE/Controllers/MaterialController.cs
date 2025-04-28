using Microsoft.AspNetCore.Mvc;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Entities;

namespace API_PROGRAMACION_DE_SOFTWARE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;
        private readonly ILogger<MaterialController> _logger;

        public MaterialController(IMaterialService materialService, ILogger<MaterialController> logger)
        {
            _materialService = materialService;
            _logger = logger;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> ListMaterials()
        {
            var materials = await _materialService.ListMaterials();
            return Ok(materials);
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> GetMaterial(int id)
        {
            var material = await _materialService.GetMaterial(id);
            if (material != null)
            {
                _logger.LogInformation($"Material con ID: {id} encontrado.");
                return Ok(material);
            }
            else
            {
                _logger.LogWarning($"No se encontró el material con ID: {id}.");
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> CreateMaterial(Material material)
        {
            bool result = await _materialService.CreateMaterial(material);
            if (result)
            {
                _logger.LogInformation("Material creado exitosamente.");
                return Ok();
            }
            else
            {
                _logger.LogError("Error al crear el material.");
                return BadRequest("No se pudo crear el material.");
            }
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> UpdateMaterial(Material material)
        {
            bool result = await _materialService.UpdateMaterial(material);
            if (result)
            {
                _logger.LogInformation($"Material con ID: {material.MaterialId} actualizado exitosamente.");
                return Ok();
            }
            else
            {
                _logger.LogError($"Error al actualizar el material con ID: {material.MaterialId}.");
                return BadRequest("No se pudo actualizar el material.");
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            bool result = await _materialService.DeleteMaterial(id);
            if (result)
            {
                _logger.LogInformation($"Material con ID: {id} eliminado exitosamente.");
                return NoContent();
            }
            else
            {
                _logger.LogError($"Error al eliminar el material con ID: {id}.");
                return NotFound();
            }
        }
    }
}