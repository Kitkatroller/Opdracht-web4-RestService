namespace ReservatieBeheer.Beheerder.API.DTOs
{
    public class RestaurantDto
    {
        public string Naam { get; set; }

        // LocatieID wordt gebruikt als je alleen met het ID van de locatie werkt.
        // Als je echter de volledige locatiegegevens wilt ontvangen of versturen, gebruik je een LocatieDto.
        public LocatieDto Locatie { get; set; }

        public string Keuken { get; set; }

        public string Telefoon { get; set; }

        public string Email { get; set; }

        // Voeg eventuele andere velden toe die relevant zijn voor je operaties.
    }

}
