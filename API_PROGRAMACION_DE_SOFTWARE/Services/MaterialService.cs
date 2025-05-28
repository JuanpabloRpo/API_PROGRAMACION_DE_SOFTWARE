using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using Microsoft.IdentityModel.Tokens;
using API_PROGRAMACION_DE_SOFTWARE.Migrations;
using API_PROGRAMACION_DE_SOFTWARE.Controllers;

namespace API_PROGRAMACION_DE_SOFTWARE.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialDAO _materialDAO;
        private readonly ILogger<MaterialController> _logger;

        public MaterialService(IMaterialDAO materialDAO, ILogger<MaterialController> logger)
        {
            _materialDAO = materialDAO;
            _logger = logger;
        }

        public async Task<List<Material>> ListMaterials()
        {
            var materials = await _materialDAO.ListMaterials();
            return materials.IsNullOrEmpty() != true ? materials: null;
        }

        public async Task<List<Material>> ListAvailableMaterials()
        {
            var materials = await _materialDAO.ListAvailableMaterials();
            return materials.IsNullOrEmpty() != true ? materials : null;
        }

        public async Task<Material> GetMaterial(int materialId)
        {
            
            var material = await _materialDAO.GetMaterial(materialId);
            if (material != null)
            {
                _logger.LogInformation($"Material con ID: {materialId} encontrado.");
                
            }
            else
            {
                _logger.LogWarning($"No se encontró el material con ID: {materialId}.");
                
            }
            return material;
        }

        public async Task<Boolean> CreateMaterial(Material material)
        {
            
            var result = await _materialDAO.CreateMaterial(material);
            if (result)
            {
                _logger.LogInformation("Material creado exitosamente.");
                return true;
            }
            else
            {
                _logger.LogError("Error al crear el material.");
                return false;
            }
        }


        public async Task<Boolean> UpdateMaterial(Material material)
        {
            
            var result = await _materialDAO.UpdateMaterial(material);
            if (result)
            {
                _logger.LogInformation($"Material con ID: {material.MaterialId} actualizado exitosamente.");
                return true;
            }
            else
            {
                _logger.LogError($"Error al actualizar el material con ID: {material.MaterialId}.");
                return false;
            }
        }

        public async Task<Boolean> DeleteMaterial(int materialId)
        {
            var result = await _materialDAO.DeleteMaterial(materialId);
            if (result)
            {
                _logger.LogInformation($"Material con ID: {materialId} eliminado exitosamente.");
                return true;
            }
            else
            {
                _logger.LogError($"Error al eliminar el material con ID: {materialId}.");
                return false;
            }
        }

    }
}
