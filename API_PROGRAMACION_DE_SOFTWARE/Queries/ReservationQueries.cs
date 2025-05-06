namespace API_PROGRAMACION_DE_SOFTWARE.Queries
{
    public class ReservationQueries
    {
        public static string listReservations = "SELECT * FROM Reservations";
        public static string getReservation = @"SELECT * FROM Reservations WHERE ReservationId = @ReservationId";
        public static string checkReservationPending = @"
            SELECT * 
            FROM Reservations 
            WHERE ReservationId = @ReservationId AND Status IN (0, 5)";
        public static string updateReservationStatus = @"
            UPDATE Reservations 
            SET Status = @Status 
            WHERE ReservationId = @ReservationId";
        public static string searchReservationsUser = @"SELECT * FROM Reservations WHERE UserId = @userId";
        public static string createReservation = @"
            INSERT INTO Reservations (UserId, MaterialId, RequestDate, ExpirationDate, Status) 
            VALUES (@UserId, @MaterialId, GETDATE(), DATEADD(DAY, 7, GETDATE()), @Status); 
            SELECT CAST(SCOPE_IDENTITY() as int)";
        public static string extendReservation = @"
            UPDATE Reservations 
            SET RequestDate = @RequestDate, ExpirationDate = @ExpirationDate, Status = @Status 
            WHERE ReservationId = @ReservationId";
        public static string deleteReservation = @"DELETE FROM Reservations WHERE ReservationId = @ReservationId";
    }
}
