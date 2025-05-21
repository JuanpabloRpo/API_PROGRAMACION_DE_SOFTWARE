using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API_PROGRAMACION_DE_SOFTWARE.Queries
{
    public class UserQueries
    {
        public static string listUsers = "SELECT * FROM Users";
        public static string getUser = "SELECT * FROM Users WHERE Id = @Id";
        public static string createUser = "INSERT INTO Users (Document, FirstName, LastName, MiddleName, Age, Email, UserName, Password, TypeUser, Role, Arrears, IsActive) VALUES (@Document, @FirstName, @LastName, @MiddleName, @Age, @Email, @UserName, @Password, @TypeUser, @Role, @Arrears, @IsActive); SELECT CAST(SCOPE_IDENTITY() as int)";
        public static string updateUser = "UPDATE Users SET Document = @Document, FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, Age = @Age, Email = @Email, UserName = @UserName, Password = @Password, Arrears = @Arrears, TypeUser = @TypeUser, Role = @Role, IsActive = @IsActive WHERE Id = @Id";
        public static string deleteUser = "DELETE FROM Users WHERE Id = @Id";
    }
}
