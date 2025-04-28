using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;

namespace API_PROGRAMACION_DE_SOFTWARE.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationDAO _reservationDAO;

        public ReservationService(IReservationDAO reservationDAO)
        {
            _reservationDAO = reservationDAO;
        }

        public async Task<List<Reservation>> ListReservations()
        {
            List<Reservation> result = await _reservationDAO.ListReservations();

            return result;
        }

        public async Task<Reservation> GetReservation(int reservationId)
        {
            Reservation result = await _reservationDAO.GetReservation(reservationId);
            return result;
        }

        public async Task<Boolean> CreateReservation(Reservation reservation)
        {
            return await _reservationDAO.CreateReservation(reservation);
        }

        public async Task<Boolean> UpdateReservation(Reservation reservation)
        {
            return await _reservationDAO.UpdateReservation(reservation);
        }

        public async Task<Boolean> DeleteReservation(int reservationId)
        {
            return await _reservationDAO.DeleteReservation(reservationId);
        }
    }
}
