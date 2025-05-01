using Microsoft.AspNetCore.Mvc;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;

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
            return Ok(await _materialService.ListMaterials());
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> GetMaterial(int materialId)
        {
            var material = await _materialService.GetMaterial(materialId);
            if (material != null)
            {
                _logger.LogInformation($"Material con ID: {materialId} encontrado.");
                return Ok(material);
            }
            else
            {
                _logger.LogWarning($"No se encontró el material con ID: {materialId}.");
                return NotFound();
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<List<Material>> ViewAvaraibleMaterials()
        {
            return null;
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
        public async Task<IActionResult> DeleteMaterial(int materialId)
        {
            bool result = await _materialService.DeleteMaterial(materialId);
            if (result)
            {
                _logger.LogInformation($"Material con ID: {materialId} eliminado exitosamente.");
                return NoContent();
            }
            else
            {
                _logger.LogError($"Error al eliminar el material con ID: {materialId}.");
                return NotFound();
            }
        }
    }
}