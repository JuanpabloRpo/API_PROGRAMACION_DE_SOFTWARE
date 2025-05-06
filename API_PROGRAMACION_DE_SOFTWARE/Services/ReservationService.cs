using API_PROGRAMACION_DE_SOFTWARE.Controllers;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Enumerations;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Migrations;

namespace API_PROGRAMACION_DE_SOFTWARE.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationDAO _reservationDAO;
        private readonly ILogger<ReservationController> _logger;
        private readonly IMaterialDAO _materialDAO;

        public ReservationService(IReservationDAO reservationDAO, ILogger<ReservationController> logger, IMaterialDAO materialDAO)
        {
            _reservationDAO = reservationDAO;
            _logger = logger;
            _materialDAO = materialDAO;
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
                _logger.LogInformation($"Se encontraron las reservas del usuario con ID: {UserId}");
                return result;
            }
            else
            {
                _logger.LogWarning($"No se encontró reservas para el usuario con ID: {UserId}.");
                return null;
            }
        }

        public async Task<Boolean> CreateReservation(int materialId, int userId)
        {

            bool resultCheck = await _materialDAO.CheckAvailableMaterial(materialId);
            if (!resultCheck)
            {
                _logger.LogError("Material NO disponible/Existe");
                return false;
            }

            Reservation reservation = new Reservation();
            reservation.UserId = userId;
            reservation.MaterialId = materialId;
            reservation.RequestDate = DateTime.Now;
            reservation.ExpirationDate = reservation.RequestDate.AddDays(7);
            reservation.Status = ReservationStatus.Pending;

            var resultado = await _reservationDAO.CreateReservation(reservation);
            var materialActualizado = await _materialDAO.UpdateMaterialStatus(materialId, 1);

            if (resultado == true && materialActualizado == true)
            {
                _logger.LogInformation("Reserva creada de manera exitosa.");

                return true;
            }
            else
            {
                _logger.LogError("Error al crear la reserva.");
                return false;
            }
        }

        public async Task<Boolean> ExtendReservation(int reservationId, int userId) 
        {
            var reservation = await _reservationDAO.GetReservation(reservationId);
            if (reservation == null)
            {
                _logger.LogError($"No se encontró la reserva con ID: {reservationId}.");
                return false;
            }

            if (reservation.UserId != userId)
            {
                _logger.LogError($"La reserva con ID: {reservationId} no pertenece al usuario con ID: {userId}.");
                return false;
            }
            if (reservation.Status == ReservationStatus.Extended)
            {
                _logger.LogWarning($"La reserva con ID: {reservationId} ya fue extendida previamente.");
                return false;
            }
            reservation.RequestDate = DateTime.Now;
            reservation.ExpirationDate = reservation.RequestDate.AddDays(7);
            reservation.Status = ReservationStatus.Extended;
            var resultado = await _reservationDAO.ExtendReservation(reservation);
            if (resultado)
            {
                _logger.LogInformation("Reserva extendida de manera exitosa.");
                return true;
            }
            else
            {
                _logger.LogError($"Error al extender la reserva con ID: {reservation.ReservationId}.");
                return false;
            }
        }

        public async Task<Boolean> RejectReservation(int reservationId, int userId) 
        {
            var reservation = await _reservationDAO.GetReservation(reservationId);
            if (reservation == null)
            {
                _logger.LogError($"No se encontró la reserva con ID: {reservationId}.");
                return false;
            }

            if (reservation.UserId != userId)
            {
                _logger.LogError($"La reserva con ID: {reservationId} no pertenece al usuario con ID: {userId}.");
                return false;
            }
            reservation.Status = ReservationStatus.Rejected;
            var resultado = await _reservationDAO.UpdateReservationStatus(reservation.ReservationId, (int)reservation.Status);
            var materialResult = await _materialDAO.UpdateMaterialStatus(reservation.MaterialId, 0);

            if (resultado)
            {
                _logger.LogInformation("Reserva cancelada de manera exitosa.");
                return true;
            }
            else
            {
                _logger.LogError($"Error al cancelar la reserva con ID: {reservation.ReservationId}.");
                return false;
            }
        }

        public async Task<Boolean> CancelReservation(int reservationId, int userId)
        {
            var reservation = await _reservationDAO.GetReservation(reservationId);
            if (reservation == null)
            {
                _logger.LogError($"No se encontró la reserva con ID: {reservationId}.");
                return false;
            }

            if (reservation.UserId != userId)
            {
                _logger.LogError($"La reserva con ID: {reservationId} no pertenece al usuario con ID: {userId}.");
                return false;
            }
            reservation.Status = ReservationStatus.Canceled;
            var resultado = await _reservationDAO.UpdateReservationStatus(reservation.ReservationId, (int)reservation.Status);
            var materialResult = await _materialDAO.UpdateMaterialStatus(reservation.MaterialId, 0);

            if (resultado)
            {
                _logger.LogInformation("Reserva cancelada de manera exitosa.");
                return true;
            }
            else
            {
                _logger.LogError($"Error al cancelar la reserva con ID: {reservation.ReservationId}.");
                return false;
            }
        }

        public async Task<Boolean> DeleteReservation(int reservationId)
        {
            
            var resultado = await _reservationDAO.DeleteReservation(reservationId);
            if (resultado)
            {
                _logger.LogInformation($"Reserva con ID: {reservationId} eliminada de manera exitosa.");
                return true;
            }
            else
            {
                _logger.LogError($"Error al eliminar la reserva con ID: {reservationId}.");
                return false;
            }
        }
    }
}
