using API_PROGRAMACION_DE_SOFTWARE.Controllers;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Migrations;

namespace API_PROGRAMACION_DE_SOFTWARE.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationDAO _reservationDAO;
        private readonly ILogger<MaterialController> _logger;

        public ReservationService(IReservationDAO reservationDAO, ILogger<MaterialController> logger)
        {
            _reservationDAO = reservationDAO;
            _logger = logger;
        }

        public async Task<List<Reservation>> ListReservations()
        {
            List<Reservation> result = await _reservationDAO.ListReservations();

            return result;
        }

        public async Task<Reservation> GetReservation(int reservationId)
        {
            var reservation = await _reservationDAO.GetReservation(reservationId);
            if (reservation != null)
            {
                _logger.LogInformation($"Reserva con ID: {reservationId} encontrado.");
                return reservation;
            }
            else
            {
                _logger.LogWarning($"No se encontró la reserva con ID: {reservationId}.");
                return null;
            }
            
        }

        public async Task<List<Reservation>> GetReservationsUser(int UserId)
        {
            var result = await _reservationDAO.SearchReservationsUser(UserId);
            if (result != null)
            {
                _logger.LogInformation($"Reserva con ID: {UserId} encontrado.");
                return result;
            }
            else
            {
                _logger.LogWarning($"No se encontró la reserva con ID: {UserId}.");
                return null;
            }
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
