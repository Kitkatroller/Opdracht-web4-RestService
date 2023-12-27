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
    }
}
