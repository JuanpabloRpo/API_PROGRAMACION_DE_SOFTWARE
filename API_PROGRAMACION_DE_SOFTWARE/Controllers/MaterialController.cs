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

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> ListMaterials()
        {
            var listM = await _materialService.ListMaterials();
            return listM != null ? Ok(listM) : NotFound(listM);
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> GetMaterial(int materialId)
        {
            var material = await _materialService.GetMaterial(materialId);
            return material != null ? Ok(material) : NotFound("No se encontro el material");
        }

        [HttpGet]
        [Route("MaterialesDisponibles")]
        public async Task<IActionResult> ViewAvaraibleMaterials()
        {
            var listM = await _materialService.ListAvailableMaterials();
            return listM != null ? Ok(listM) : NotFound(listM);
        }

        [HttpPost]
        [Route("CrearLibro")]
        public async Task<IActionResult> CreateMaterial(Book material)
        {
            return await _materialService.CreateMaterial(material) ? Ok("Material creado") : BadRequest("No se pudo crear el material.");
        }

        [HttpPost]
        [Route("CrearAudioVisual")]
        public async Task<IActionResult> CreateMaterial(Audiovisual material)
        {
            return await _materialService.CreateMaterial(material) ? Ok("Material creado") : BadRequest("No se pudo crear el material.");
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> UpdateMaterial(Material material)
        {
            return await _materialService.UpdateMaterial(material) ? Ok("Material actualizado"): BadRequest("No se pudo actualizar el material.");
        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> DeleteMaterial(int materialId)
        {
            return await _materialService.DeleteMaterial(materialId) ? Ok("Material eliminado"): NotFound("No se pudo eliminar el material");
        }

    }
}