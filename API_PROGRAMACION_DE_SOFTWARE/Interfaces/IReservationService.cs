using API_PROGRAMACION_DE_SOFTWARE.Entities;

namespace API_PROGRAMACION_DE_SOFTWARE.Interfaces
{
    public interface IReservationService
    {
        Task<List<Reservation>> ListReservations();
        Task<Reservation> GetReservation(int reservationId);
        Task<List<Reservation>> GetReservationsUser(int UserId);
        Task<Boolean> CreateReservation(int materialId, int userId);
        Task<Boolean> ExtendReservation(int reservationId, int userId);
        Task<Boolean> RejectReservation(int reservationId, int userId);
        Task<Boolean> CancelReservation(int reservationId, int userId);

        Task<Boolean> DeleteReservation(int reservationId);
    }
}
