namespace API_PROGRAMACION_DE_SOFTWARE.Queries
{
    public class ReservationQueries
    {
        public static string listReservations = "SELECT ReservationId, UserId, MaterialId, RequestDate, ExpirationDate FROM Reservations";
        public static string getReservation = @"SELECT ReservationId, UserId, MaterialId, RequestDate, ExpirationDate FROM Reservations WHERE ReservationId = @ReservationId";
        public static string searchReservationsUser = @"SELECT ReservationId, UserId, MaterialId, RequestDate, ExpirationDate FROM Reservations WHERE UserId = @userId";
        public static string createReservation = @"INSERT INTO Reservations (UserId, MaterialId, RequestDate, ExpirationDate, Status) VALUES (@UserId, @MaterialId, GETDATE(), DATEADD(DAY, 7, GETDATE()), @Status); SELECT CAST(SCOPE_IDENTITY() as int)";
        public static string updateReservation = @"UPDATE Reservations SET UserId = @UserId, MaterialId = @MaterialId, RequestDate = @RequestDate, ExpirationDate = @ExpirationDate, Status = @Status WHERE ReservationId = @ReservationId";
        public static string deleteReservation = @"DELETE FROM Reservations WHERE ReservationId = @ReservationId";
    }
}
