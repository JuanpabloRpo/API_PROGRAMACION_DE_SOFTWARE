using API_PROGRAMACION_DE_SOFTWARE.Entities;

namespace API_PROGRAMACION_DE_SOFTWARE.Interfaces
{
    public interface IReservationService
    {
        Task<List<Reservation>> ListReservations();
        Task<Reservation> GetReservation(int reservationId);
        Task<List<Reservation>> GetReservationsUser(int UserId);
        Task<Boolean> CreateReservation(int materialId, int userId);
        Task<Boolean> ExtendReservation(Reservation reservation);
        Task<Boolean> RejectReservation(Reservation reservation);
        Task<Boolean> CancelReservation(Reservation reservation);

        Task<Boolean> DeleteReservation(int reservationId);
    }
}
