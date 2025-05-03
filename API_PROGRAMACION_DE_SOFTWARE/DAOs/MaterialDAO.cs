using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Queries;
using API_PROGRAMACION_DE_SOFTWARE.Utilities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace API_PROGRAMACION_DE_SOFTWARE.Services
{
    public class MaterialDAO : IMaterialDAO
    {
        private readonly SQLServerConfiguration _connectionString;
        private readonly ILogger<MaterialDAO> _logger;

        public MaterialDAO(IOptions<SQLServerConfiguration> connectionString, ILogger<MaterialDAO> logger)
        {
            _connectionString = connectionString.Value;
            _logger = logger;
        }

        private SqlConnection Connection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public async Task<List<Material>> ListMaterials()
        {
            List<Material> result = new List<Material>();
            try
            {
                using var db = Connection();
                var materials = await db.QueryAsync<Material>(MaterialQueries.listMaterials, new { });
                return materials.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al leer materiales: {ex.Message}");
            }
            return result;
        }
        public async Task<List<Material>> ListAvailableMaterials()
        {
            List<Material> result = new List<Material>();
            try
            {
                using var db = Connection();
                var materials = await db.QueryAsync<Material>(MaterialQueries.listAvaraibleMaterials, new { });
                return materials.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al leer materiales: {ex.Message}");
            }
            return result;
        }

        public async Task<Material> GetMaterial(int materialId)
        {
            try
            {
                using var db = Connection();
                var material = await db.QueryFirstOrDefaultAsync<Material>(MaterialQueries.getMaterial, new { MaterialId = materialId });
                _logger.LogInformation($"Búsqueda exitosa del material con ID: {materialId} en SQL Server");

                if (material == null)
                {
                    throw new InvalidOperationException($"No se encontró un usuario con el ID: {materialId}");
                }

                return material;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el material con ID {materialId}: {ex.Message}");
                throw;
            }
        }
        public async Task<Boolean> CheckAvailableMaterial(int materialId)
        {
            try
            {
                using var db = Connection();
                var material = await db.QueryFirstOrDefaultAsync<Material>(MaterialQueries.checkAvailableMaterial, new { MaterialId = materialId });
                return material != null ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar el material con ID {materialId}: {ex.Message}");
            }
            return false;
        }
        public async Task<Boolean> UpdateMaterialStatus(int materialId, int newStatus)
        {
            var rowsAffected = 0;
            try
            {
                using var db = Connection();
                rowsAffected = await db.ExecuteAsync(MaterialQueries.updateMaterialStatus, new { MaterialId = materialId, Status = newStatus });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el Status del material con ID {materialId}: {ex.Message}");
            }
            return rowsAffected > 0;
        }

        public async Task<bool> CreateMaterial(Material material)
        {
            try
            {
                using var db = Connection();
                var parameters = new
                {
                    material.Title,
                    material.Author,
                    material.PublicationYear,
                    material.Status,
                    material.Condition,
                    material.Topic,
                    Discriminator = material.GetType().Name,
                    Format = material is Audiovisual audiovisual ? audiovisual.Format : null,
                    Duration = material is Audiovisual audiovisual2 ? audiovisual2.Duration : null,
                    Pages = material is Book book ? book.Pages : (int?)null
                };

                var id = await db.ExecuteScalarAsync<int>(MaterialQueries.createMaterial, parameters);
                return id > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el material: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateMaterial(Material material)
        {
            try
            {
                using var db = Connection();
                var parameters = new
                {
                    material.MaterialId,
                    material.Title,
                    material.Author,
                    material.PublicationYear,
                    material.Status,
                    material.Condition,
                    material.Topic,
                    Discriminator = material.GetType().Name,
                    Format = material is Audiovisual audiovisual ? audiovisual.Format : null,
                    Duration = material is Audiovisual audiovisual2 ? audiovisual2.Duration : null,
                    Pages = material is Book book ? book.Pages : (int?)null
                };

                var rowsAffected = await db.ExecuteAsync(MaterialQueries.updateMaterial, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el material: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteMaterial(int materialID)  
        {
            try
            {
                using var db = Connection();
                var rowsAffected = await db.ExecuteAsync(MaterialQueries.deleteMaterial, new { MaterialId = materialID });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el material con ID {materialID}: {ex.Message}");
                throw;
            }
        }
    }
}
