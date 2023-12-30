using ReservatieBeheer.BL.Models;
using System.ComponentModel.DataAnnotations;

namespace ReservatieBeheer.Beheerder.API.DTOs
{
    public class ReservatieDto
    {
        [Required(ErrorMessage = "U moet minstens 1 of meer plaatsen zoeken.")]
        [Range(1, 1000, ErrorMessage = "Het aantal plaatsen moet minstens 1 zijn.")]
        public int AantalPlaatsen { get; set; }

        [Required(ErrorMessage = "U moet een datum kiezen")]
        public DateTime Datum { get; set; }
    }
}
