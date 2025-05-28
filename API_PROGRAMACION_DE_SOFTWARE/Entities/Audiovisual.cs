using API_PROGRAMACION_DE_SOFTWARE.Enumerations;

namespace API_PROGRAMACION_DE_SOFTWARE.Entities
{
    public class Audiovisual : Material
    {
        public required string Format { get; set; }
        public required string Duration { get; set; }
        public Audiovisual() { }

        public Audiovisual(string title, string author, int publicationYear, MaterialStatus status, MaterialCondition condition, MaterialTopic topic) : base(title, author, publicationYear, status, condition, topic)
        {
        }
    }
}
