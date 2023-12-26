using ReservatieBeheer.BL.Models;
using ReservatieBeheer.DL.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.DL.Mappers
{
    public class RestaurantMapper
    {        
        public static Restaurant MapToBLModel(RestaurantEF efRestaurantEntry)
        {
            if (efRestaurantEntry == null) return null;

            return new Restaurant
            {
                ID = efRestaurantEntry.ID,
                Naam = efRestaurantEntry.Naam,
                LocatieID = efRestaurantEntry.LocatieID,
                Locatie = LocatieMapper.MapToBLModel(efRestaurantEntry.Locatie),
                Keuken = efRestaurantEntry.Keuken,
                Telefoon = efRestaurantEntry.Telefoon,
                Email = efRestaurantEntry.Email
            };
        }

        public static RestaurantEF MapToEfEntity(Restaurant restaurantEntry)
        {
            if (restaurantEntry == null) return null;

            return new RestaurantEF
            {
                ID = restaurantEntry.ID,
                Naam = restaurantEntry.Naam,
                LocatieID = restaurantEntry.LocatieID,
                Locatie = LocatieMapper.MapToEfEntity(restaurantEntry.Locatie),
                Keuken = restaurantEntry.Keuken,
                Telefoon = restaurantEntry.Telefoon,
                Email = restaurantEntry.Email
            };
        }   
    }
}
