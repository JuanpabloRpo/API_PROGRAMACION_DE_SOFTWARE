using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Entities;

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
            return await _materialDAO.ListMaterials();
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
