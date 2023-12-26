using ReservatieBeheer.BL.Interfaces;
using ReservatieBeheer.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _restaurantRepo.VerwijderRestaurant(restaurantId);
        }
        public void UpdateRestaurant(Restaurant restaurant)
        {
            var restau = _restaurantRepo.GetRestaurantById(restaurant.ID);
            if (restau == null)
            {
                throw new KeyNotFoundException("Klant niet gevonden");
            }
            _restaurantRepo.UpdateRestaurant(restaurant);
        }
    }
}
