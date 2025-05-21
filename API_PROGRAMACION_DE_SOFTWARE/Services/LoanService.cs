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
        private readonly IReservationDAO _reservationDAO;
        private readonly IMaterialDAO _materialDAO;

        public LoanService(ILoanDAO loanDAO, ILogger<LoanController> logger, IReservationDAO reservationDAO, IMaterialDAO materialDAO)
        {
            _loanDAO = loanDAO;
            _logger = logger;
            _reservationDAO = reservationDAO;
            _materialDAO = materialDAO;
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
        public async Task<List<Loan>> GetLoansUser(int UserId)
        {
            var result = await _loanDAO.SearchLoansUser(UserId);
            if (result != null)
            {
                _logger.LogInformation($"Se encontraron los prestamos del usuario con ID: {UserId}");
                return result;
            }
            else
            {
                _logger.LogWarning($"No se encontró prestamos para el usuario con ID: {UserId}.");
                return null;
            }
        }

        public async Task<Boolean> CreateLoan(int reservationId, int userId)
        {
            // la reservacion tiene que estar en pendiente 
            if (!await _reservationDAO.CheckReservationPending(reservationId))
            {
                _logger.LogError("Reservacion NO Pendiente/Existe");
                return false;
            }

            // Se crea el nuevo prestamo
            var resultado = await _loanDAO.CreateLoan(reservationId, userId);

            // Se actualiza la reservacion de pending a accepted
            var reservationActualizado = await _reservationDAO.UpdateReservationStatus(reservationId, 1);

            // Se trae la reservacion para saber el Id del material para cambiar su Status
            var reservacion = await _reservationDAO.GetReservation(reservationId);
            var materialActualizado = await _materialDAO.UpdateMaterialStatus(reservacion.MaterialId, 2);

            if (resultado && reservationActualizado && materialActualizado)
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

        public async Task<Boolean> ReturnLoan(int loanId, int userId)
        {
            var loan = await _loanDAO.GetLoan(loanId);
            if (loan == null)
            {
                _logger.LogError($"No se encontró el préstamo con ID: {loanId}.");
                return false;
            }
            if (loan.UserId != userId)
            {
                _logger.LogError($"El préstamo con ID: {loanId} no pertenece al usuario con ID: {userId}.");
                return false;
            }
            loan.ReturnDate = DateTime.Now;
            bool resultado = await _loanDAO.ReturnLoan(loan);
            if (resultado)
            {
                var reservacion = await _reservationDAO.GetReservation(loan.ReservationId);
                var materialActualizado = await _materialDAO.UpdateMaterialStatus(reservacion.MaterialId, 0);
                if (materialActualizado)
                {
                    _logger.LogInformation("Préstamo devuelto de manera exitosa.");
                    return true;
                }
                else
                {
                    _logger.LogError("Error al actualizar el estado del material.");
                    return false;
                }
            }
            else
            {
                _logger.LogError($"Error al devolver el préstamo con ID: {loan.LoanId}.");
                return false;
            }
        }

        public async Task<Boolean> CancelLoan(int loanId, int userId)
        {
            var loan = await _loanDAO.GetLoan(loanId);
            if (loan == null)
            {
                _logger.LogError($"No se encontró el préstamo con ID: {loanId}.");
                return false;
            }
            if (loan.UserId != userId)
            {
                _logger.LogError($"El préstamo con ID: {loanId} no pertenece al usuario con ID: {userId}.");
                return false;
            }
            bool resultado = await _loanDAO.CancelLoan(loan);
            if (resultado)
            {
                var reservacion = await _reservationDAO.GetReservation(loan.ReservationId);
                var materialActualizado = await _materialDAO.UpdateMaterialStatus(reservacion.MaterialId, 0);
                if (materialActualizado)
                {
                    _logger.LogInformation("Préstamo cancelado de manera exitosa.");
                    return true;
                }
                else
                {
                    _logger.LogError("Error al actualizar el estado del material.");
                    return false;
                }
            }
            else
            {
                _logger.LogError($"Error al cancelar el préstamo con ID: {loan.LoanId}.");
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
