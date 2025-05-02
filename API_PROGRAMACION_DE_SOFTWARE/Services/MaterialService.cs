using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using Microsoft.IdentityModel.Tokens;

namespace API_PROGRAMACION_DE_SOFTWARE.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialDAO _materialDAO;

        public MaterialService(IMaterialDAO materialDAO)
        {
            _materialDAO = materialDAO;
        }

        public async Task<List<Material>> ListMaterials()
        {
            var materials = await _materialDAO.ListMaterials();
            return materials.IsNullOrEmpty() != true ? materials: null;
        }
        public async Task<List<Material>> ListAvaraibleMaterials()
        {
            var materials = await _materialDAO.ListAvaraibleMaterials();
            return materials.IsNullOrEmpty() != true ? materials : null;
        }

        public async Task<Material> GetMaterial(int materialID)
        {
            return await _materialDAO.GetMaterial(materialID);
        }

        public async Task<bool> CreateMaterial(Material material)
        {
            return await _materialDAO.CreateMaterial(material);
        }

        public async Task<bool> UpdateMaterial(Material material)
        {
            return await _materialDAO.UpdateMaterial(material);
        }

        public async Task<bool> DeleteMaterial(int materialID)
        {
            return await _materialDAO.DeleteMaterial(materialID);
        }
    }
}
