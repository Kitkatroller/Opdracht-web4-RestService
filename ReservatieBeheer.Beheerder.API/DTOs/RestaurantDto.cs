using System.ComponentModel.DataAnnotations;

namespace ReservatieBeheer.Beheerder.API.DTOs
{
    public class RestaurantDto
    {
        [Required(ErrorMessage = "De naam van het restaurant is vereist.")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Locatie informatie is vereist.")]
        public LocatieDto Locatie { get; set; }

        [Required(ErrorMessage = "Keuken type is vereist.")]
        public string Keuken { get; set; }

        [Required(ErrorMessage = "Telefoonnummer is vereist.")]
        [RegularExpression(@"^\+?\d[\d\s\-]*$", ErrorMessage = "Ongeldig telefoonnummer.")]
        public string Telefoon { get; set; }

        [Required(ErrorMessage = "E-mailadres is vereist.")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailadres.")]
        public string Email { get; set; }
    }

}
