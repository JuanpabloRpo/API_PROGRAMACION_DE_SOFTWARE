using API_PROGRAMACION_DE_SOFTWARE.Enumerations;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace API_PROGRAMACION_DE_SOFTWARE.Entities
{
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }
        [Required]
        [MaxLength(255)]
        public required string Title { get; set; }
        [Required]
        [MaxLength(255)]
        public required string Author { get; set; }
        [Required]
        public required int PublicationYear { get; set; }
        public MaterialStatus Status { get; set; }
        public MaterialCondition Condition { get; set; }
        public MaterialTopic Topic { get; set; }

        public Material() { }

        public Material(string title, string author, int publicationYear, MaterialStatus status, MaterialCondition condition, MaterialTopic topic)
        {
            Title = title;
            Author = author;
            PublicationYear = publicationYear;
            Status = status;
            Condition = condition;
            Topic = topic;
        }
    }
}
