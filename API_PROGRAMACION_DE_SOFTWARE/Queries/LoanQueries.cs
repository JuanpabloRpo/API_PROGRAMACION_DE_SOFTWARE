namespace API_PROGRAMACION_DE_SOFTWARE.Queries
{
    public class LoanQueries
    {
        public static string listLoans = @"SELECT * FROM Loans";

        public static string getLoan = @"SELECT * FROM Loans WHERE LoanId = @LoanId";

        public static string createLoan = @"
            INSERT INTO Loans (UserId, ReservationID, StartDate, DueDate, Status) 
            VALUES (@UserId, @ReservationID, GETDATE(), DATEADD(DAY, 7, GETDATE()), @Status); 
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string returnLoan = @"
            UPDATE Loans
            SET ReturnDate = @ReturnDate,
                Status = @Status
            WHERE LoanId = @LoanId;";

        public static string cancelLoan = @"
            UPDATE Loans
            SET Status = @Status
            WHERE LoanId = @LoanId;";

        public static string deleteLoan = @"
            DELETE FROM Loans 
            WHERE LoanId = @LoanId;";
        public static string searchLoansUser = @"SELECT * FROM Loans WHERE UserId = @userId";
    }
}
