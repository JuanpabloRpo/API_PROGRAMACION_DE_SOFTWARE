using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            if (loan != null)
            {
                _logger.LogInformation($"Préstamo con ID: {loanId} encontrado.");
                return Ok(loan);
            }
            else
            {
                _logger.LogWarning($"No se encontró el préstamo con ID: {loanId}.");
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> CreateLoan(Loan loan)
        {
            bool resultado = await _loanService.CreateLoan(loan);
            if (resultado)
            {
                _logger.LogInformation("Préstamo creado de manera exitosa.");
                return Ok();
            }
            else
            {
                _logger.LogError("Error al crear el préstamo.");
                return BadRequest("No se pudo guardar el préstamo.");
            }
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> UpdateLoan(Loan loan)
        {
            bool resultado = await _loanService.UpdateLoan(loan);
            if (resultado)
            {
                _logger.LogInformation("Préstamo actualizado de manera exitosa.");
                return Ok();
            }
            else
            {
                _logger.LogError($"Error al actualizar el préstamo con ID: {loan.LoanId}.");
                return BadRequest("No se pudo actualizar el préstamo.");
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> DeleteLoan(int loanId)
        {
            bool resultado = await _loanService.DeleteLoan(loanId);
            if (resultado)
            {
                _logger.LogInformation($"Préstamo con ID: {loanId} eliminado de manera exitosa.");
                return NoContent();
            }
            else
            {
                _logger.LogError($"Error al eliminar el préstamo con ID: {loanId}.");
                return NotFound();
            }
        }
    }
}
