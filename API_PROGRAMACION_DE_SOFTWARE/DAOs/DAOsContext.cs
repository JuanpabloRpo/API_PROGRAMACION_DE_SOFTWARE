using Microsoft.EntityFrameworkCore;
using API_PROGRAMACION_DE_SOFTWARE.Entities;

namespace API_PROGRAMACION_DE_SOFTWARE.DAOs
{
    public class DAOsContext : DbContext
    {
        public DAOsContext(DbContextOptions<DAOsContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Audiovisual> Audiovisuals { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
