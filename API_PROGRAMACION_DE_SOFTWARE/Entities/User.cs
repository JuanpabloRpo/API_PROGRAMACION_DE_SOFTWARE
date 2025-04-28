using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using API_PROGRAMACION_DE_SOFTWARE.Enumerations;


namespace API_PROGRAMACION_DE_SOFTWARE.Entities
{
    public class User : Person
    {
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public required string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public required string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Password { get; set; }

        [Required]
        public UserType TypeUser { get; set; }

        public UserRole Role { get; set; } = UserRole.Guest;

        public int Arrears { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public User() { }
    }
}
