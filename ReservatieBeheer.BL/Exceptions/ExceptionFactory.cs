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
        public static Exception CreateInvalidTimeRangeException(DateTime beginTijd, DateTime eindTijd)
        {
            return new Exception($"Invalid Time Range: The start time {beginTijd} cannot be after the end time {eindTijd}.");
        }
        public static Exception CreateReservationNotFoundException(int reservatieId)
        {
            return new Exception($"Reservation Not Found: Reservation with ID {reservatieId} does not exist.");
        }

        public static Exception CreateInvalidNewDateException(DateTime nieuweDatum)
        {
            return new Exception($"Invalid New Date: The provided new date '{nieuweDatum}' is in the past.");
        }

        public static Exception CreateInvalidNumberOfPlacesException(int nieuwAantalPlaatsen)
        {
            return new Exception($"Invalid Number of Places: The provided number of places '{nieuwAantalPlaatsen}' is less than 1.");
        }
        public static Exception CreateInvalidPostcodeException(string message)
        {
            return new Exception($"Invalid Postcode: {message}");
        }
        public static Exception CreateInvalidParameterException(string message)
        {
            return new Exception($"Invalid Parameter: {message}");
        }
        public static Exception CreateRestaurantNotFoundException(int restaurantId)
        {
            return new Exception($"Restaurant with ID {restaurantId} does not exist.");
        }
    }

}
