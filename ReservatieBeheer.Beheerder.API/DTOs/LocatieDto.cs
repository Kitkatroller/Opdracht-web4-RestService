using System.ComponentModel.DataAnnotations;

namespace ReservatieBeheer.Beheerder.API.DTOs
{
    public class LocatieDto
    {
        [Required]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Postcode moet 4 cijfers bevatten")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Gemeentenaam is verplicht")]
        public string Gemeente { get; set; }

        public string Straatnaam { get; set; }

        public string Huisnummerlabel { get; set; }
    }

}
