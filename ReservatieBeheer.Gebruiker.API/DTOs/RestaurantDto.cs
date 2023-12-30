using ReservatieBeheer.BL.Models;

namespace ReservatieBeheer.Gebruiker.API.DTOs
{
    public class RestaurantDto
    {
        public int ID { get; set; }
        public string Naam { get; set; }

        public string Keuken { get; set; }

        public string Telefoon { get; set; }

        public string Email { get; set; }
    }
}
