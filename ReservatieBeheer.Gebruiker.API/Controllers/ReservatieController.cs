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

        [HttpPost("maakReservatie/{klantId}/{TafelNummer}")]
        public IActionResult MaakReservatie(int klantId, ReservatieDto reservatie, int TafelNummer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _reservatieService.MaakReservatie(klantId, reservatie.AantalPlaatsen, reservatie.Datum, TafelNummer);
                return Ok("Reservatie gemaakt");
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Invalid Reservation Time"))
                {
                    return BadRequest(ex.Message);
                }
                else if (ex.Message.Contains("Invalid New Date"))
                {
                    return BadRequest(ex.Message);
                }
                else if (ex.Message.StartsWith("Table Not Available"))
                {
                    return Conflict(ex.Message);
                }
                else if (ex.Message.StartsWith("Customer Not Found"))
                {
                    return NotFound(ex.Message);
                }
                else
                {
                    return StatusCode(500, "Interne serverfout: " + ex.Message);
                }
            }
        }

        [HttpPut("pasReservatieAan/{reservatieId}")]
        public IActionResult PasReservatieAan(int reservatieId, ReservatieDto reservatie)
        {
            try
            {
                _reservatieService.PasReservatieAan(reservatieId, reservatie.Datum, reservatie.AantalPlaatsen);
                return Ok("Reservatie succesvol aangepast");
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Invalid Reservation Time"))
                {
                    return BadRequest(ex.Message);
                }
                else if (ex.Message.Contains("Reservation Not Found"))
                {
                    return NotFound(ex.Message);
                }
                else if (ex.Message.Contains("Invalid New Date"))
                {
                    return BadRequest(ex.Message);
                }
                else if (ex.Message.Contains("Invalid Number of Places"))
                {
                    return BadRequest(ex.Message);
                }
                else if (ex.Message.Contains("Table Not Available"))
                {
                    return BadRequest(ex.Message);
                }
                else
                {
                    return StatusCode(500, "Interne serverfout: " + ex.Message);
                }
            }
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
            try
            {
                var reservaties = _reservatieService.ZoekReservaties(klantId, beginDatum, eindDatum);

                if (reservaties == null || !reservaties.Any())
                {
                    return NotFound("Geen reservaties gevonden.");
                }

                var reservatieDtos = reservaties.Select(r => new ReservatieDto
                {
                    AantalPlaatsen = r.AantalPlaatsen,
                    Datum = r.Datum
                }).ToList();

                return Ok(reservatieDtos);
            }
            catch (Exception ex) 
            {
                if (ex.Message.StartsWith("Customer Not Found"))
                {
                    return NotFound(ex.Message);
                }
                else
                {
                    return StatusCode(500, "Interne serverfout: " + ex.Message);
                }

            }
        }
    }
}
