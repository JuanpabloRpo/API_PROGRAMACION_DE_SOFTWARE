using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace API_PROGRAMACION_DE_SOFTWARE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly ILogger<LoanController> _logger;

        public LoanController(ILoanService loanService, ILogger<LoanController> logger)
        {
            _loanService = loanService;
            _logger = logger;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<List<Loan>> ListLoans()
        {
            var loans = await _loanService.ListLoans();
            return loans;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> GetLoan(int loanId)
        {
            var loan = await _loanService.GetLoan(loanId);
            return loan == null ? BadRequest("No se encontro el prestamo") : Ok(loan);
            
        }

        [HttpGet]
        [Route("PrestamosUsuario")]
        public async Task<IActionResult> GetLoansUser(int userId)
        {
            var loan = await _loanService.GetLoansUser(userId);
            return loan == null ? BadRequest("El usuario no tiene prestamos") : Ok(loan);

        }

        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> CreateLoan(int reservationId, int userId)
        {
            var resultado = await _loanService.CreateLoan(reservationId, userId);
            return resultado != true ? BadRequest("No se pudo crear el préstamo.") : Ok("Prestamo creado");
        }

        [HttpPut]
        [Route("Devolver")]
        public async Task<IActionResult> ReturnLoan(int loanId, int userId)
        {
            var resultado = await _loanService.ReturnLoan(loanId, userId);
            return resultado == true ? Ok(resultado) : BadRequest("No se pudo actualizar el préstamo.");
        }

        [HttpPut]
        [Route("Cancelar")]
        public async Task<IActionResult> CancelLoan(int loanId, int userId)
        {
            var resultado = await _loanService.CancelLoan(loanId, userId);
            return resultado == true ? Ok(resultado) : BadRequest("No se pudo cancelar el préstamo.");
        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> DeleteLoan(int loanId)
        {
            var resultado = await _loanService.DeleteLoan(loanId);
            return resultado == true ? Ok() : NotFound();
        }

    }
}
