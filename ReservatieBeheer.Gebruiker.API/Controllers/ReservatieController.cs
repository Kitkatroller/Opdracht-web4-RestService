using Microsoft.AspNetCore.Mvc;
using ReservatieBeheer.BL.Services;
using ReservatieBeheer.Gebruiker.API.DTOs;

namespace ReservatieBeheer.Gebruiker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservatieController : Controller
    {
        private readonly ReservatieService _reservatieService;

        public ReservatieController(ReservatieService reservatieService)
        {
            _reservatieService = reservatieService;
        }

        [HttpPost("maakReservatie")]
        public IActionResult MaakReservatie(ReservatieDto reservatie)
        {
            // Valideer de input parameters
            _reservatieService.MaakReservatie(reservatie.klantId,
                reservatie.AantalPlaatsen,
                reservatie.Datum,
                reservatie.TafelNummer);

            return Ok("Reservatie gemaakt");
        }
        [HttpPut("pasReservatieAan/{reservatieId}")]
        public IActionResult PasReservatieAan(int reservatieId, ReservatieDto reservatie)
        {
            if (!_reservatieService.PasReservatieAan(reservatieId, reservatie.Datum, reservatie.AantalPlaatsen))
            {
                return BadRequest("Aanpassing van de reservatie is niet mogelijk. Controleer de beschikbaarheid van de tafel op de nieuwe datum en tijd.");
            }

            return Ok("Reservatie succesvol aangepast");
        }

    }
}
