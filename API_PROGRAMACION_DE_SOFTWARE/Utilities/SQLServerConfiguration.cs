namespace API_PROGRAMACION_DE_SOFTWARE.Utilities
{
    public class SQLServerConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;

        public SQLServerConfiguration()
        {
        }

        public SQLServerConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
