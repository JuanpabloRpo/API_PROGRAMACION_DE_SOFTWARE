using API_PROGRAMACION_DE_SOFTWARE.Enumerations;
using Microsoft.VisualBasic;

namespace API_PROGRAMACION_DE_SOFTWARE.Entities
{
    public class Material
    {
        public int MaterialId { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required int PublicationYear { get; set; }
        public MaterialStatus Status { get; set; }
        public MaterialCondition Condition { get; set; }
        public MaterialTopic Topic { get; set; }

        public Material() { }
    }
}
