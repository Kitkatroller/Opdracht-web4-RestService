using Microsoft.EntityFrameworkCore;
using ReservatieBeheer.BL.Interfaces;
using ReservatieBeheer.BL.Models;
using ReservatieBeheer.DL.EFModels;
using ReservatieBeheer.DL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.DL.Repositories
{
    public class RestaurantRepo : IRestaurantRepo
    {
        private readonly ReservatieBeheerContext _context;

        public RestaurantRepo(ReservatieBeheerContext context)
        {
            _context = context;
        }
        public void VoegRestaurantToe(Restaurant restaurant)
        {
            try
            {
                RestaurantEF restaurantEF = RestaurantMapper.MapToEfEntity(restaurant);
                _context.Locaties.Add(restaurantEF.Locatie);
                _context.SaveChanges();

                // Update de locatie ID op de klant
                restaurantEF.LocatieID = restaurant.Locatie.ID;

                _context.Restaurants.Add(restaurantEF);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Overweeg een specifiekere foutafhandeling of log de fout
                throw new Exception("Fout bij het toevoegen van het restaurant: " + ex.Message);
            }
        }
        public void VerwijderRestaurant(int restaurantId)
        {
            var restaurant = _context.Restaurants.Find(restaurantId);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                _context.SaveChanges();
            }
        }
        public void UpdateRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Update(RestaurantMapper.MapToEfEntity(restaurant));
            _context.SaveChanges();
        }

        public Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurantEf = _context.Restaurants
                .Include(r => r.Locatie) // Eager loading van de Locatie, indien nodig
                .FirstOrDefault(r => r.ID == restaurantId);

            return restaurantEf != null ? RestaurantMapper.MapToBLModel(restaurantEf) : null;
        }
    }
}
