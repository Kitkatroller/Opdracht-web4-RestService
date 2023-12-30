using ReservatieBeheer.BL.Exceptions;
using ReservatieBeheer.BL.Interfaces;
using ReservatieBeheer.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReservatieBeheer.BL.Services
{
    public class RestaurantService
    {
        private readonly IRestaurantRepo _restaurantRepo;

        public RestaurantService(IRestaurantRepo restaurantRepo)
        {
            _restaurantRepo = restaurantRepo;
        }

        public void VoegRestaurantToe(Restaurant restaurant)
        {
            _restaurantRepo.VoegRestaurantToe(restaurant);
        }
        public void VerwijderRestaurant(int restaurantId)
        {
            if (!_restaurantRepo.DoesRestaurantExist(restaurantId))
            {
                throw ExceptionFactory.CreateRestaurantNotFoundException(restaurantId);
            }
            _restaurantRepo.VerwijderRestaurant(restaurantId);
        }
        public void UpdateRestaurant(Restaurant restaurant)
        {
            if (!_restaurantRepo.DoesRestaurantExist(restaurant.ID))
            {
                throw ExceptionFactory.CreateRestaurantNotFoundException(restaurant.ID);
            }
            var restau = _restaurantRepo.GetRestaurantById(restaurant.ID);

            restaurant.LocatieID = restau.LocatieID;
            restaurant.Locatie.ID = restau.LocatieID;

            _restaurantRepo.UpdateRestaurant(restaurant);
        }

        public IEnumerable<Restaurant> ZoekRestaurants(string postcode, string keuken)
        {
            if (!Regex.IsMatch(postcode, @"^\d{4}$"))
            {
                throw ExceptionFactory.CreateInvalidPostcodeException("Postcode moet 4 cijfers bevatten.");
            }

            return _restaurantRepo.ZoekRestaurants(postcode, keuken);
        }
        public IEnumerable<(string Naam, string Keuken, Tafel Tafel)> VindBeschikbareRestaurants(int aantalPersonen, DateTime tijd)
        {
            if (aantalPersonen <= 0)
            {
                throw ExceptionFactory.CreateInvalidParameterException("Aantal personen moet groter zijn dan 0.");
            }
            if (tijd <= DateTime.Now)
            {
                throw ExceptionFactory.CreateInvalidParameterException("De opgegeven tijd moet in de toekomst liggen.");
            }
            return _restaurantRepo.VindGeschikteTafelsPerRestaurant(aantalPersonen, tijd);
        }
    }
}
