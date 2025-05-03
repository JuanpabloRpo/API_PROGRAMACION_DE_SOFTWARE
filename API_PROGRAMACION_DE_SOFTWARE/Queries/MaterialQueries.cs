namespace API_PROGRAMACION_DE_SOFTWARE.Queries
{
    public class MaterialQueries
    {
        public static string listMaterials = "SELECT * FROM Materials";
        public static string listAvaraibleMaterials = "SELECT * FROM Materials WHERE Status = 0";
        public static string getMaterial = @"SELECT MaterialID, Title, Author, PublicationYear, Status, Condition, Topic, Discriminator, Format, Duration, Pages FROM Materials WHERE MaterialId = @MaterialId";
        public static string checkAvailableMaterial = @"SELECT * FROM Materials WHERE MaterialId = @MaterialId AND Status = 0";
        public static string updateMaterialStatus = @"UPDATE Materials SET Status = @Status WHERE MaterialId = @MaterialId";
        public static string createMaterial = @"INSERT INTO Materials (Title, Author, PublicationYear, Status, Condition, Topic, Discriminator, Format, Duration, Pages) VALUES (@Title, @Author, @PublicationYear, @Status, @Condition, @Topic, @Discriminator, @Format, @Duration, @Pages); SELECT CAST(SCOPE_IDENTITY() as int)";
        public static string updateMaterial = @"UPDATE Materials SET Title = @Title, Author = @Author, PublicationYear = @PublicationYear, Status = @Status, Condition = @Condition, Topic = @Topic, Discriminator = @Discriminator, Format = @Format, Duration = @Duration, Pages = @Pages WHERE materialID = @MaterialId";
        public static string deleteMaterial = @"DELETE FROM Materials WHERE MaterialID = @MaterialId";
    }
}
