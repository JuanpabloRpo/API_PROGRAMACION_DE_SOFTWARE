using API_PROGRAMACION_DE_SOFTWARE.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_PROGRAMACION_DE_SOFTWARE.Entities
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        [Required]
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        [Required]
        [ForeignKey(nameof(MaterialId))]
        public int MaterialId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public ReservationStatus Status { get; set; }

        public Reservation() { }
    }
}
