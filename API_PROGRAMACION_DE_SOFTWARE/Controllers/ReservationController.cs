using Microsoft.AspNetCore.Mvc;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;

namespace API_PROGRAMACION_DE_SOFTWARE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(IReservationService reservationService, ILogger<ReservationController> logger)
        {
            _reservationService = reservationService;
            _logger = logger;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> ListReservations()
        {
            var reservations = await _reservationService.ListReservations();
            return Ok(reservations);
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> GetReservation(int reservationId)
        {
            var reservation = await _reservationService.GetReservation(reservationId);
            if (reservation != null)
            {
                _logger.LogInformation($"Reserva con ID: {reservationId} encontrado.");
                return Ok(reservation);
            }
            else
            {
                _logger.LogWarning($"No se encontró la reserva con ID: {reservationId}.");
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> CreateReservation(Reservation reservation)
        {
            bool resultado = await _reservationService.CreateReservation(reservation);
            if (resultado)
            {
                _logger.LogInformation("Reserva creada de manera exitosa.");
                return Ok();
            }
            else
            {
                _logger.LogError("Error al crear la reserva.");
                return BadRequest("No se pudo guardar la reserva.");
            }
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> UpdateReservation(Reservation reservation)
        {
            bool resultado = await _reservationService.UpdateReservation(reservation);
            if (resultado)
            {
                _logger.LogInformation("Reserva actualizada de manera exitosa.");
                return Ok();
            }
            else
            {
                _logger.LogError($"Error al actualizar la reserva con ID: {reservation.ReservationId}.");
                return BadRequest("No se pudo actualizar la reserva.");
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> DeleteReservation(int reservationId)
        {
            bool resultado = await _reservationService.DeleteReservation(reservationId);
            if (resultado)
            {
                _logger.LogInformation($"Reserva con ID: {reservationId} eliminada de manera exitosa.");
                return NoContent();
            }
            else
            {
                _logger.LogError($"Error al eliminar la reserva con ID: {reservationId}.");
                return NotFound();
            }
        }
    }
}
