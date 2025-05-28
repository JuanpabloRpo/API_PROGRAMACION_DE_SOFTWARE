using API_PROGRAMACION_DE_SOFTWARE.Enumerations;

namespace API_PROGRAMACION_DE_SOFTWARE.Entities
{
    public class Book:Material
    {
        public required int Pages { get; set; }
        public Book() { }

        public Book(string title, string author, int publicationYear, MaterialStatus status, MaterialCondition condition, MaterialTopic topic) : base(title, author, publicationYear, status, condition, topic)
        {
        }
    }
}
