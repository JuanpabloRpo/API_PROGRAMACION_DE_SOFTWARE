using API_PROGRAMACION_DE_SOFTWARE.Entities;

namespace API_PROGRAMACION_DE_SOFTWARE.Interfaces
{
    public interface IMaterialDAO
    {
        Task<List<Material>> ListMaterials();
        Task<List<Material>> ListAvailableMaterials();
        Task<Material> GetMaterial(int id);
        Task<Boolean> CreateMaterial(Material material);
        Task<Boolean> CheckAvailableMaterial(int materialId);
        Task<Boolean> UpdateMaterialStatus(int materialId, int newStatus);
        Task<Boolean> UpdateMaterial(Material material);
        Task<Boolean> DeleteMaterial(int id);
    }
}
