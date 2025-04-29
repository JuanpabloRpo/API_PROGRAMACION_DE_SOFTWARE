using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;

namespace API_PROGRAMACION_DE_SOFTWARE.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanDAO _loanDAO;

        public LoanService(ILoanDAO loanDAO)
        {
            _loanDAO = loanDAO;
        }

        public async Task<List<Loan>> ListLoans()
        {
            List<Loan> result = await _loanDAO.ListLoans();
            return result;
        }

        public async Task<Loan> GetLoan(int loanId)
        {
            Loan result = await _loanDAO.GetLoan(loanId);
            return result;
        }

        public async Task<Boolean> CreateLoan(Loan loan)
        {
            return await _loanDAO.CreateLoan(loan);
        }

        public async Task<Boolean> UpdateLoan(Loan loan)
        {
            return await _loanDAO.UpdateLoan(loan);
        }

        public async Task<Boolean> DeleteLoan(int loanId)
        {
            return await _loanDAO.DeleteLoan(loanId);
        }
    }
}
