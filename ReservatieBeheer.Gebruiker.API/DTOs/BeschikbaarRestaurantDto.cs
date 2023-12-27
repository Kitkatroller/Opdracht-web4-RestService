namespace ReservatieBeheer.Gebruiker.API.DTOs
{
    public class BeschikbaarRestaurantDto
    {
        public string Naam { get; set; }
        public string Keuken { get; set; }
        public int AantalPlaatsen { get; set; } // Aantal plaatsen van de beschikbare tafel

    }
}
