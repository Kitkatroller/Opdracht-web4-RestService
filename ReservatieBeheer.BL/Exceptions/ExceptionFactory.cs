using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.BL.Exceptions
{
    public static class ExceptionFactory
    {
        public static Exception CreateInvalidReservationTimeException(string message)
        {
            return new Exception($"Invalid Reservation Time: {message}");
        }

        public static Exception CreateTableNotAvailableException(string message)
        {
            return new Exception($"Table Not Available: {message}");
        }

        public static Exception CreateCustomerNotFoundException(int klantId)
        {
            return new Exception($"Customer Not Found: Klant with ID {klantId} does not exist.");
        }
        public static Exception CreateTableNotFoundException(int tafelNummer)
        {
            return new Exception($"Table Not Found: Table with number {tafelNummer} does not exist.");
        }
    }

}
