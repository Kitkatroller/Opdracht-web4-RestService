using Microsoft.AspNetCore.Mvc;
using ReservatieBeheer.Gebruiker.API.DTOs;
using ReservatieBeheer.BL.Models;
using ReservatieBeheer.BL.Services;
using ReservatieBeheer.DL.EFModels;

namespace ReservatieBeheer.Gebruiker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : Controller
    {
        private readonly RestaurantService _restaurantService;
        private readonly ILogger<GebruikerController> _logger;

        public RestaurantController(RestaurantService restaurantService, ILogger<GebruikerController> logger)
        {
            _restaurantService = restaurantService;
            _logger = logger;
        }

        [HttpGet("zoek")]
        public IActionResult ZoekRestaurants(string postcode, string keuken)
        {
            _logger.LogInformation($"ZoekRestaurants aangeroepen met postcode: {postcode}, keuken: {keuken}");

            try
            {
                var restaurants = _restaurantService.ZoekRestaurants(postcode, keuken);
                if (restaurants == null || !restaurants.Any())
                {
                    return NotFound("Geen restaurants gevonden met de opgegeven criteria.");
                }

                var restaurantDtos = restaurants.Select(r => new RestaurantDto
                {
                    ID = r.ID,
                    Naam = r.Naam,
                    Keuken = r.Keuken,
                    Telefoon = r.Telefoon,
                    Email = r.Email
                }).ToList();

                return Ok(restaurantDtos);
            }
            catch (Exception ex) when (ex.Message.StartsWith("Invalid Postcode"))
            {
                _logger.LogError($"Fout bij ZoekRestaurants: {ex.Message}");

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Interne serverfout: " + ex.Message);
            }
        }
        //[HttpGet("beschikbaar")]
        //public ActionResult<IEnumerable<BeschikbaarRestaurantDto>> VindBeschikbareRestaurants(int aantalPersonen, DateTime tijd)
        //{
        //    var beschikbareRestaurants = _restaurantService.VindBeschikbareRestaurants(aantalPersonen, tijd);

        //    var beschikbareRestaurantsDto = beschikbareRestaurants.Select(r => new BeschikbaarRestaurantDto
        //    {
        //        Naam = r.Naam,
        //        Keuken = r.Keuken,
        //        AantalPlaatsen = r.Tafel.Aantal
        //    });

        //    return Ok(beschikbareRestaurantsDto);
        //}
        [HttpGet("beschikbaar")]
        public ActionResult<IEnumerable<BeschikbaarRestaurantDto>> VindBeschikbareRestaurants(int aantalPersonen, DateTime tijd)
        {
            _logger.LogInformation($"VindBeschikbareRestaurants aangeroepen met aantalPersonen: {aantalPersonen}, tijd: {tijd}");

            try
            {
                var beschikbareRestaurants = _restaurantService.VindBeschikbareRestaurants(aantalPersonen, tijd);

                var beschikbareRestaurantsDto = beschikbareRestaurants.Select(r => new BeschikbaarRestaurantDto
                {
                    Naam = r.Naam,
                    Keuken = r.Keuken,
                    AantalPlaatsen = r.Tafel.Aantal
                });

                return Ok(beschikbareRestaurantsDto);
            }
            catch (Exception ex) when (ex.Message.StartsWith("Invalid Parameter"))
            {
                _logger.LogError($"Fout bij VindBeschikbareRestaurants: {ex.Message}");

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Interne serverfout: " + ex.Message);
            }
        }
    }
}
