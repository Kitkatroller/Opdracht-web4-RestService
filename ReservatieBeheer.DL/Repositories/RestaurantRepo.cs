using Microsoft.EntityFrameworkCore;
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
                    // Mark the restaurant as inactive instead of deleting
                    restaurant.IsActive = false;

                    // Save the changes to the database
                    _context.SaveChanges();
                }
            }
        }


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
                    .FirstOrDefault(r => r.ID == restaurantId && r.IsActive);

                return restaurantEf != null ? RestaurantMapper.MapToBLModel(restaurantEf) : null;
            }
        }


        public IEnumerable<Restaurant> ZoekRestaurants(string postcode, string keuken)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
            {
                var query = _context.Restaurants.AsQueryable();

                if (!string.IsNullOrEmpty(postcode))
                {
                    query = query.Where(r => r.Locatie.Postcode == postcode);
                }

                if (!string.IsNullOrEmpty(keuken))
                {
                    query = query.Where(r => r.Keuken.ToLower().Contains(keuken.ToLower()));
                }

                return query.Select(r => RestaurantMapper.MapToBLModel(r)).ToList();
            }
        }

        //public IEnumerable<(string Naam, string Keuken, Tafel Tafel)> VindGeschikteTafelsPerRestaurant(int aantalPersonen)
        //{
        //    using (var _context = _dbContextFactory.CreateDbContext())
        //    {
        //        var resultaat = _context.Restaurants
        //            .Select(r => new
        //            {
        //                r.Naam,
        //                r.Keuken,
        //                GeschikteTafelEF = r.Tafels
        //                    .Where(t => !t.Reserved && t.Aantal >= aantalPersonen)
        //                    .OrderBy(t => t.Aantal)
        //                    .FirstOrDefault()
        //            })
        //            .Where(r => r.GeschikteTafelEF != null)
        //            .ToList()
        //            .Select(r => (r.Naam, r.Keuken, Tafel: TafelMapper.MapToBLModel(r.GeschikteTafelEF)));

        //        return resultaat;
        //    }
        //}

        public IEnumerable<(string Naam, string Keuken, Tafel Tafel)> VindGeschikteTafelsPerRestaurant(int aantalPersonen, DateTime gewensteTijd)
        {
            var eindTijd = gewensteTijd.AddHours(1.5);

            using (var _context = _dbContextFactory.CreateDbContext())
            {
                // Fetch restaurants with suitable tables directly
                var resultaat = _context.Restaurants
                    .Select(r => new
                    {
                        r.Naam,
                        r.Keuken,
                        GeschikteTafelEF = r.Tafels
                            .OrderBy(t => t.Aantal)
                            .FirstOrDefault(t => t.Aantal >= aantalPersonen &&
                                                 !_context.Reservaties.Any(res => res.TafelNummer == t.TafelNummer &&
                                                                                  ((res.Datum >= gewensteTijd && res.Datum < eindTijd) ||
                                                                                   (res.Datum.AddHours(1.5) > gewensteTijd && res.Datum < eindTijd))))
                    })
                    .Where(r => r.GeschikteTafelEF != null)
                    .ToList()
                    .Select(r => (r.Naam, r.Keuken, Tafel: TafelMapper.MapToBLModel(r.GeschikteTafelEF)))
                    .ToList();

                return resultaat;
            }
        }
        public bool DoesRestaurantExist(int restaurantId)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
            {
                return _context.Restaurants.Any(restaurant => restaurant.ID == restaurantId && restaurant.IsActive);
            }
        }

    }
}
