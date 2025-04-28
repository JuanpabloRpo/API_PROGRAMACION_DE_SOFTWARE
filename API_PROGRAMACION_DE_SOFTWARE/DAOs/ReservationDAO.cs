using API_PROGRAMACION_DE_SOFTWARE.Controllers;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Queries;
using API_PROGRAMACION_DE_SOFTWARE.Utilities;
using API_PROGRAMACION_DE_SOFTWARE.Enumerations;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

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
                _logger.LogInformation($"Búsqueda exitosa de la reserva con ID: {reservationId} en SQL Server");

                if (reservation == null)
                {
                    throw new InvalidOperationException($"No se encontró una reserva con el ID: {reservationId}");
                }

                return reservation;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar la reserva con ID: {reservationId} en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server (buscar): {ex.Message}");
                throw;
            }
        }

        public async Task<Boolean> CreateReservation(Reservation reservation)
        {
            reservation.RequestDate = DateTime.Now;
            reservation.ExpirationDate = reservation.RequestDate.AddDays(7);
            reservation.Status = ReservationStatus.Pending;
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
                    Status = reservation.Status.ToString()
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

        public async Task<Boolean> UpdateReservation(Reservation reservation)
        {
            reservation.RequestDate = DateTime.Now;
            reservation.ExpirationDate = reservation.RequestDate.AddDays(7);
            reservation.Status = ReservationStatus.Pending;
            int result = 0;
            try
            {
                using var db = Connection();
                result = await db.ExecuteAsync(ReservationQueries.updateReservation, new
                {
                    reservation.ReservationId,
                    reservation.UserId,
                    reservation.MaterialId,
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
