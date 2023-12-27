using Microsoft.AspNetCore.Mvc;
using ReservatieBeheer.Gebruiker.API.DTOs;
using ReservatieBeheer.BL.Models;
using ReservatieBeheer.BL.Services;

namespace ReservatieBeheer.Gebruiker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : Controller
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("zoek")]
        public IActionResult ZoekRestaurants(string postcode, string keuken)
        {
            var restaurants = _restaurantService.ZoekRestaurants(postcode, keuken);
            return Ok(restaurants);
        }
        [HttpGet("beschikbaar")]
        public ActionResult<IEnumerable<BeschikbaarRestaurantDto>> VindBeschikbareRestaurants(int aantalPersonen)
        {
            var beschikbareRestaurants = _restaurantService.VindBeschikbareRestaurants(aantalPersonen);

            var beschikbareRestaurantsDto = beschikbareRestaurants.Select(r => new BeschikbaarRestaurantDto
            {
                Naam = r.Naam,
                Keuken = r.Keuken,
                AantalPlaatsen = r.Tafel.Aantal
            });

            return Ok(beschikbareRestaurantsDto);
        }
    }
}
