using Microsoft.AspNetCore.Mvc;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using Microsoft.IdentityModel.Tokens;

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
            return reservation != null ? Ok(reservation) : NotFound(reservation);

        }

        [HttpGet]
        [Route("ReservacionesUsuario")]
        public async Task<IActionResult> GetReservationsUser(int userId)
        {
            var reservation = await _reservationService.GetReservationsUser(userId);
            return reservation.IsNullOrEmpty() != true ? Ok(reservation) : NotFound("No hay reservas");

        }

        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> CreateReservation(int materialId, int userId)
        {
            return await _reservationService.CreateReservation(materialId,userId) == true ? Ok("Reserva creada de manera exitosa.") : BadRequest("No se pudo guardar la reserva.");
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> UpdateReservation(Reservation reservation)
        {
            var resultado = await _reservationService.UpdateReservation(reservation);
            return resultado != false ? Ok(resultado) : BadRequest("No se pudo actualizar la reserva.");
            
        }


        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> DeleteReservation(int reservationId)
        {
            return await _reservationService.DeleteReservation(reservationId) == true ? NoContent():NotFound();
        }
    }
}
