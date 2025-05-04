using API_PROGRAMACION_DE_SOFTWARE.Controllers;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Queries;
using API_PROGRAMACION_DE_SOFTWARE.Utilities;
using API_PROGRAMACION_DE_SOFTWARE.Enumerations;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using API_PROGRAMACION_DE_SOFTWARE.Migrations;

namespace API_PROGRAMACION_DE_SOFTWARE.DAOs
{
    public class ReservationDAO : IReservationDAO
    {
        private readonly SQLServerConfiguration _connectionString;
        private readonly ILogger<ReservationController> _logger;

        public ReservationDAO(IOptions<SQLServerConfiguration> connectionString, ILogger<ReservationController> logger)
        {
            _connectionString = connectionString.Value;
            _logger = logger;
        }

        protected SqlConnection Connection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public async Task<List<Reservation>> ListReservations()
        {
            List<Reservation> result = new List<Reservation>();

            try
            {
                using var db = Connection();
                IEnumerable<Reservation> lista = await db.QueryAsync<Reservation>(ReservationQueries.listReservations, new { });
                _logger.LogInformation("Consulta exitosa a SQL Server");
                return lista.ToList(); ;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al leer la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server: {ex.Message}");
            }

            return result;
        }

        public async Task<Reservation> GetReservation(int reservationId)
        {
            try
            {
                using var db = Connection();
                var reservation = await db.QueryFirstOrDefaultAsync<Reservation>(ReservationQueries.getReservation, new { ReservationId = reservationId });
                return reservation;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar la reserva con ID: {reservationId} en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server (buscar): {ex.Message}");
            }
            return null;
        }

        public async Task<Boolean> CheckReservationPending(int reservationId)
        {
            try
            {
                using var db = Connection();
                var reservation = await db.QueryFirstOrDefaultAsync<Reservation>(ReservationQueries.checkReservationPending, new { ReservationId = reservationId });
                return reservation != null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar la reserva con ID: {reservationId} en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server (buscar): {ex.Message}");
            }
            return false;
        }
        public async Task<Boolean> UpdateReservationStatus(int reservationId, int newStatus)
        {
            var rowsAffected = 0;
            try
            {
                using var db = Connection();
                rowsAffected = await db.ExecuteAsync(ReservationQueries.updateReservationStatus, new { ReservationId = reservationId, Status = newStatus });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el Status de la reserva con ID {reservationId}: {ex.Message}");
            }
            return rowsAffected > 0;
        }

        public async Task<List<Reservation>> SearchReservationsUser(int userId)
        {
            try
            {
                using var db = Connection();
                var reservation = await db.QueryAsync<Reservation>(ReservationQueries.searchReservationsUser, new { UserId = userId });
                return reservation.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar la reserva para el usuario con ID: {userId} en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server (buscar): {ex.Message}");

            }
            return null;
        }

        public async Task<Boolean> CreateReservation(Reservation reservation)
        {
            int result = 0;
            try
            {
                using var db = Connection();
                result = await db.ExecuteAsync(ReservationQueries.createReservation, new
                {
                    reservation.UserId,
                    reservation.MaterialId,
                    reservation.RequestDate,
                    reservation.ExpirationDate,
                    Status = ConversorEnumInt.ReservationStatusConver(ReservationStatus.Pending.ToString())
                });
                return result > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al insertar en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server: {ex.Message}");
            }
            return result > 0;
        }

        public async Task<Boolean> ExtendReservation(Reservation reservation)
        {
            reservation.RequestDate = DateTime.Now;
            reservation.ExpirationDate = reservation.RequestDate.AddDays(7);
            reservation.Status = ReservationStatus.Pending;
            int result = 0;
            try
            {
                using var db = Connection();
                result = await db.ExecuteAsync(ReservationQueries.extendReservation, new
                {
                    reservation.ReservationId,
                    reservation.RequestDate,
                    reservation.ExpirationDate,
                    reservation.Status
                });
                return result > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server: {ex.Message}");
            }
            return result > 0;
        }

        public async Task<Boolean> RejectReservation(Reservation reservation)
        {
            int result = 0;
            try
            {
                using var db = Connection();
                result = await db.ExecuteAsync(ReservationQueries.rejectReservation, new
                {
                    reservation.ReservationId,
                    reservation.Status
                });
                return result > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server: {ex.Message}");
            }
            return result > 0;
        }

        public async Task<Boolean> CancelReservation(Reservation reservation)
        {
            int result = 0;
            try
            {
                using var db = Connection();
                result = await db.ExecuteAsync(ReservationQueries.cancelReservation, new
                {
                    reservation.ReservationId,
                    reservation.Status
                });
                return result > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server: {ex.Message}");
            }
            return result > 0;
        }

        public async Task<bool> DeleteReservation(int reservationId)
        {
            int result = 0;
            try
            {
                using var db = Connection();
                result = await db.ExecuteAsync(ReservationQueries.deleteReservation, new { ReservationId = reservationId });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar la reserva con ID: {reservationId} de la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server (eliminar): {ex.Message}");
                return false;
            }
        }
    }
}
