using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReservatieBeheer.Beheerder.API.DTOs;
using ReservatieBeheer.BL.Services;

namespace ReservatieBeheer.Beheerder.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservatieController : Controller
    {
        private readonly ReservatieService _reservatieService;
        private readonly ILogger<ReservatieController> _logger;

        public ReservatieController(ReservatieService reservatieService, ILogger<ReservatieController> logger)
        {
            _reservatieService = reservatieService;
            _logger = logger;
        }

        [HttpGet("zoekReservatiesPerRestaurant")]
        public IActionResult ZoekReservatiesPerRestaurant(int restaurantId, DateTime? beginDatum, DateTime? eindDatum)
        {
            try
            {
                _logger.LogInformation($"Zoeken naar reservaties voor restaurant ID {restaurantId}.");
                var reservaties = _reservatieService.ZoekReservatiesPerRestaurant(restaurantId, beginDatum, eindDatum);
                if (reservaties == null || !reservaties.Any())
                {
                    return NotFound("Geen reservaties gevonden voor het opgegeven restaurant.");
                }

                var reservatieDtos = reservaties.Select(r => new ReservatieDto
                {
                    AantalPlaatsen = r.AantalPlaatsen,
                    Datum = r.Datum
                }).ToList();

                return Ok(reservatieDtos);
            }
            catch (Exception ex) when (ex.Message.Contains("Restaurant with ID"))
            {
                _logger.LogError(ex, $"Fout bij het zoeken naar reservaties: {ex.Message}");
                return NotFound(ex.Message);
            }

        }
    }
}
