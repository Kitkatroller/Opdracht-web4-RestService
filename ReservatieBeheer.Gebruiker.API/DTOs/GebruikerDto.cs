using ReservatieBeheer.BL.Models;
using System.ComponentModel.DataAnnotations;

namespace ReservatieBeheer.Gebruiker.API.DTOs
{
    public class GebruikerDto
    {
        public int KlantNummer { get; set; }

        [Required(ErrorMessage = "Naam is verplicht")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress(ErrorMessage = "Ongeldig emailadres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefoonnummer is verplicht")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Telefoonnummer moet alleen cijfers bevatten")]
        public string TelefoonNummer { get; set; }

        [Required(ErrorMessage = "Locatie is verplicht")]
        public LocatieDto Locatie { get; set; }
    }

}
