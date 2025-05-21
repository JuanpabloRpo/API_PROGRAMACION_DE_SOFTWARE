using API_PROGRAMACION_DE_SOFTWARE.Entities;

namespace API_PROGRAMACION_DE_SOFTWARE.Interfaces
{
    public interface ILoanDAO
    {
        Task<List<Loan>> ListLoans();
        Task<Loan> GetLoan(int loanId);
        Task<Boolean> CreateLoan(int reservationId, int userId);
        Task<List<Loan>> SearchLoansUser(int userId);
        Task<Boolean> ReturnLoan(Loan loan);
        Task<Boolean> CancelLoan(Loan loan);
        Task<Boolean> DeleteLoan(int loanId);
    }
}
