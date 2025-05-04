namespace API_PROGRAMACION_DE_SOFTWARE.Utilities
{
    public class ConversorEnumInt
    {
        public static int ReservationStatusConver(string status)
        {
            switch (status)
            {
                case "Pending":
                    return 0;
                case "Accepted":
                    return 1;
                case "Expired":
                    return 2;
                case "Rejected":
                    return 3;
                case "Canceled":
                    return 4;
                default: return 0;
            }
        }
        public static int LoanStatusConver(string status)
        {
            switch (status)
            {
                case "Active":
                    return 0;
                case "Overdue":
                    return 1;
                case "Completed":
                    return 2;
                case "Canceled":
                    return 3;
                default: return 0;
            }
        }
    }
}
