using API_PROGRAMACION_DE_SOFTWARE.Entities;

namespace API_PROGRAMACION_DE_SOFTWARE.Interfaces
{
    public interface IReservationDAO
    {
        Task<List<Reservation>> ListReservations();
        Task<Reservation> GetReservation(int reservationId);
        Task<Boolean> CheckReservationPending(int reservationId);
        Task<Boolean> UpdateReservationStatus(int reservationId, int newStatus);
        Task<List<Reservation>> SearchReservationsUser(int UserId);
        Task<Boolean> CreateReservation(Reservation reservation);
        Task<Boolean> UpdateReservation(Reservation reservation);
        Task<Boolean> DeleteReservation(int reservationId);
    }
}
