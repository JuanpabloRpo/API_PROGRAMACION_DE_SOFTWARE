using API_PROGRAMACION_DE_SOFTWARE.Controllers;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Migrations;

namespace API_PROGRAMACION_DE_SOFTWARE.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanDAO _loanDAO;
        private readonly ILogger<LoanController> _logger;

        public LoanService(ILoanDAO loanDAO, ILogger<LoanController> logger)
        {
            _loanDAO = loanDAO;
            _logger = logger;
        }

        public async Task<List<Loan>> ListLoans()
        {
            List<Loan> result = await _loanDAO.ListLoans();
            return result;
        }

        public async Task<Loan> GetLoan(int loanId)
        {
            
            var loan = await _loanDAO.GetLoan(loanId);
            if (loan != null)
            {
                _logger.LogInformation($"Préstamo con ID: {loanId} encontrado.");
                
            }
            else
            {
                _logger.LogWarning($"No se encontró el préstamo con ID: {loanId}.");
                
            }
            return loan;
        }

        public async Task<Boolean> CreateLoan(Loan loan)
        {
            
            bool resultado = await _loanDAO.CreateLoan(loan);
            if (resultado)
            {
                _logger.LogInformation("Préstamo creado de manera exitosa.");
                return true;
            }
            else
            {
                _logger.LogError("Error al crear el préstamo.");
                return false;
            }

        }

        public async Task<Boolean> UpdateLoan(Loan loan)
        {
            
            bool resultado = await _loanDAO.UpdateLoan(loan);
            if (resultado)
            {
                _logger.LogInformation("Préstamo actualizado de manera exitosa.");
                return true;
            }
            else
            {
                _logger.LogError($"Error al actualizar el préstamo con ID: {loan.LoanId}.");
                return false;
            }
        }

        public async Task<Boolean> DeleteLoan(int loanId)
        {
            
            var resultado = await _loanDAO.DeleteLoan(loanId);
            if (resultado)
            {
                _logger.LogInformation($"Préstamo con ID: {loanId} eliminado de manera exitosa.");
                return true;
            }
            else
            {
                _logger.LogError($"Error al eliminar el préstamo con ID: {loanId}.");
                return false;
            }
        }
    }
}
