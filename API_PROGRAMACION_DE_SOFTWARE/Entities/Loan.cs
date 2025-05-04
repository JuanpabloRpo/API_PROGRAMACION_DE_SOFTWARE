using API_PROGRAMACION_DE_SOFTWARE.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_PROGRAMACION_DE_SOFTWARE.Entities
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }
        [Required]
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        [Required]
        [ForeignKey(nameof(ReservationId))]
        public int ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public LoanStatus Status { get; set; }
        public Loan () { }
    }
}
