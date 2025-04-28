namespace API_PROGRAMACION_DE_SOFTWARE.Entities
{
    public class Audiovisual : Material
    {
        public required string Format { get; set; }
        public required string Duration { get; set; }
        public Audiovisual() { }
    }
}
