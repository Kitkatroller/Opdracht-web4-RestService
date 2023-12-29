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

        [HttpPost("maakReservatie/{klantId}")]
        public IActionResult MaakReservatie(int klantId, ReservatieDto reservatie, int TafelNummer)
        {
            if (!ModelState.IsValid)
            {               
                return BadRequest(ModelState);
            }
            try
            {
                _reservatieService.MaakReservatie(klantId,
                    reservatie.AantalPlaatsen,
                    reservatie.Datum,
                    TafelNummer);
                return Ok("Reservatie gemaakt");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Interne serverfout: " + ex.Message);
            }
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

        [HttpDelete("annuleerReservatie/{reservatieId}")]
        public IActionResult AnnuleerReservatie(int reservatieId)
        {
            if (!_reservatieService.AnnuleerReservatie(reservatieId))
            {
                return BadRequest("Reservatie kan niet geannuleerd worden of is al verstreken.");
            }

            return Ok("Reservatie succesvol geannuleerd");
        }

        [HttpGet("zoekReservaties")]
        public IActionResult ZoekReservaties(int klantId, DateTime? beginDatum, DateTime? eindDatum)
        {
            var reservaties = _reservatieService.ZoekReservaties(klantId, beginDatum, eindDatum);
            if (reservaties == null || !reservaties.Any())
            {
                return NotFound("Geen reservaties gevonden.");
            }

            return Ok(reservaties);
        }
    }
}
