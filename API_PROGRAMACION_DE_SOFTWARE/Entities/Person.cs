using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_PROGRAMACION_DE_SOFTWARE.Entities
{
    public abstract class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Document { get; set; }
        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; } 
        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }
        [MaxLength(50)]
        public string MiddleName { get; set; } = string.Empty;
        [Required]
        [Range(0, 150)]
        public int Age { get; set; }
        public Person() { }
    }
}