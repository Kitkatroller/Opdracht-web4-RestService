using Microsoft.AspNetCore.Mvc;
using ReservatieBeheer.Beheerder.API.DTOs;
using ReservatieBeheer.BL.Models;
using ReservatieBeheer.BL.Services;

namespace ReservatieBeheer.Beheerder.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : Controller
    {
        private readonly RestaurantService _restaurantService;
        private readonly ILogger<RestaurantController> _logger;

        public RestaurantController(RestaurantService restaurantService, ILogger<RestaurantController> logger)
        {
            _restaurantService = restaurantService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult VoegRestaurantToe([FromBody] RestaurantDto restaurantDto)
        {
            _logger.LogInformation("VoegRestaurantToe actie aangeroepen");
            var restaurant = new Restaurant
            {
                Naam = restaurantDto.Naam,
                Locatie = new Locatie
                {
                    Postcode = restaurantDto.Locatie.Postcode,
                    Gemeente = restaurantDto.Locatie.Gemeente,
                    Straatnaam = restaurantDto.Locatie.Straatnaam,
                    Huisnummerlabel = restaurantDto.Locatie.Huisnummerlabel
                },
                Keuken = restaurantDto.Keuken,
                Telefoon = restaurantDto.Telefoon,
                Email = restaurantDto.Email
            };

            _restaurantService.VoegRestaurantToe(restaurant);
            return Ok("Restaurant toegevoegd");
        }

        [HttpDelete("{restaurantId}")]
        public IActionResult VerwijderRestaurant(int restaurantId)
        {
            try
            {
                _logger.LogInformation($"VerwijderRestaurant actie aangeroepen voor restaurant ID {restaurantId}");
                _restaurantService.VerwijderRestaurant(restaurantId);
                return Ok($"Restaurant met ID {restaurantId} is verwijderd");
            }
            catch (Exception ex) when (ex.Message.Contains("Restaurant with ID"))
            {
                _logger.LogError(ex, $"Fout bij het verwijderen van restaurant: {ex.Message}");
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{restaurantId}")]
        public IActionResult UpdateRestaurant(int restaurantId, [FromBody] RestaurantDto restaurantDto)
        {
            try
            {
                _logger.LogInformation($"UpdateRestaurant actie aangeroepen voor restaurant ID {restaurantId}");
                var restaurant = new Restaurant
                {
                    ID = restaurantId,
                    Naam = restaurantDto.Naam,
                    Locatie = new Locatie
                    {
                        Gemeente = restaurantDto.Locatie.Gemeente,
                        Huisnummerlabel = restaurantDto.Locatie.Huisnummerlabel,
                        Postcode = restaurantDto.Locatie.Postcode,
                        Straatnaam = restaurantDto.Locatie.Straatnaam
                    },
                    Keuken = restaurantDto.Keuken,
                    Telefoon = restaurantDto.Telefoon,
                    Email = restaurantDto.Email
                };

                
                _restaurantService.UpdateRestaurant(restaurant);
                return Ok("Restaurantgegevens bijgewerkt");
            }
            catch (Exception ex) when (ex.Message.Contains("Restaurant with ID"))
            {
                _logger.LogError(ex, $"Fout bij het bijwerken van restaurant: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Interne serverfout: " + ex.Message);
            }
        }
    }
}
