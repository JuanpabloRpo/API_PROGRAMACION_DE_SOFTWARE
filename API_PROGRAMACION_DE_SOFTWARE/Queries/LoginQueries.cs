namespace API_PROGRAMACION_DE_SOFTWARE.Queries
{
    public class LoginQueries
    {
        public static string SearchUser = @"SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password";
        
    }
}
