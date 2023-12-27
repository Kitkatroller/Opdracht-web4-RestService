﻿using Microsoft.EntityFrameworkCore;
using ReservatieBeheer.BL.Interfaces;
using ReservatieBeheer.BL.Models;
using ReservatieBeheer.DL.EFModels;
using ReservatieBeheer.DL.Interfaces;
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
        private readonly IDbContextFactory<ReservatieBeheerContext> _dbContextFactory;

        public RestaurantRepo(IDbContextFactory<ReservatieBeheerContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public void VoegRestaurantToe(Restaurant restaurant)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
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
            
        }
        public void VerwijderRestaurant(int restaurantId)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
            {
                var restaurant = _context.Restaurants.Find(restaurantId);
                if (restaurant != null)
                {
                    _context.Restaurants.Remove(restaurant);
                    _context.SaveChanges();
                }
            }
            
        }
        //public void UpdateRestaurant(Restaurant restaurant)
        //{
        //    using (var _context = _dbContextFactory.CreateDbContext())
        //    {
        //        var restaurantEF = RestaurantMapper.MapToEfEntity(restaurant);

        //        if (restaurantEF.Locatie != null && restaurantEF.Locatie.ID != 0)
        //        {
        //            // Als de Locatie al bestaat (ID is ingesteld), markeer als ongewijzigd
        //            _context.Entry(restaurantEF.Locatie).State = EntityState.Unchanged;
        //        }

        //        _context.Restaurants.Update(restaurantEF);
        //        _context.SaveChanges();
        //    }
        //}

        public void UpdateRestaurant(Restaurant restaurant)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
            {
                var restaurantEF = RestaurantMapper.MapToEfEntity(restaurant);

                // Ophalen van het bestaande restaurant met de geladen Locatie
                var bestaandRestaurant = _context.Restaurants
                    .Include(r => r.Locatie)
                    .FirstOrDefault(r => r.ID == restaurantEF.ID);

                if (bestaandRestaurant != null)
                {
                    // Update de velden van het bestaande restaurant
                    _context.Entry(bestaandRestaurant).CurrentValues.SetValues(restaurantEF);

                    if (restaurantEF.Locatie != null && restaurantEF.Locatie.ID > 0)
                    {
                        // Update de velden van de bestaande locatie
                        if (bestaandRestaurant.Locatie != null)
                        {
                            _context.Entry(bestaandRestaurant.Locatie).CurrentValues.SetValues(restaurantEF.Locatie);
                        }
                        else
                        {
                            // Als de locatie nog niet bestaat in het bestaande restaurant
                            bestaandRestaurant.Locatie = restaurantEF.Locatie;
                        }
                    }

                    _context.SaveChanges();
                }
            }
        }



        public Restaurant GetRestaurantById(int restaurantId)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
            {
                var restaurantEf = _context.Restaurants
                    .Include(r => r.Locatie) // Eager loading van de Locatie, indien nodig
                    .FirstOrDefault(r => r.ID == restaurantId);

                return restaurantEf != null ? RestaurantMapper.MapToBLModel(restaurantEf) : null;
            }
            
        }
    }
}
