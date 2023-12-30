using Microsoft.AspNetCore.Mvc;
using ReservatieBeheer.Beheerder.API.DTOs;
using ReservatieBeheer.BL.Services;

namespace ReservatieBeheer.Beheerder.API.Controllers
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

        [HttpGet("zoekReservatiesPerRestaurant")]
        public IActionResult ZoekReservatiesPerRestaurant(int restaurantId, DateTime? beginDatum, DateTime? eindDatum)
        {
            try
            {
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
                return NotFound(ex.Message);
            }

        }
    }
}
