using ReservatieBeheer.BL.Models;

namespace ReservatieBeheer.Gebruiker.API.DTOs
{
    public class ReservatieDto
    {
        public int klantId { get; set; }

        public int AantalPlaatsen { get; set; }

        public DateTime Datum { get; set; }

        public int Uur { get; set; }

        public int TafelNummer { get; set; }
    }
}
