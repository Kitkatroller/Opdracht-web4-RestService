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

        public RestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost]
        public IActionResult VoegRestaurantToe([FromBody] RestaurantDto restaurantDto)
        {
            var restaurant = new Restaurant
            {
                Naam = restaurantDto.Naam,
                Locatie = new Locatie
                {
                    // Stel de Locatie eigenschappen in op basis van LocatieDto
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
            _restaurantService.VerwijderRestaurant(restaurantId);
            return Ok($"Restaurant met ID {restaurantId} is verwijderd");
        }

        [HttpPut("{restaurantId}")]
        public IActionResult UpdateRestaurant(int restaurantId, [FromBody] RestaurantDto restaurantDto)
        {
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
    }
}
