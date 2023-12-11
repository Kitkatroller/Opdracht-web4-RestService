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



            return new Restaurant();
        }

        public static RestaurantEF MapToEfEntity(Restaurant RestaurantEntry)
        {
            if (RestaurantEntry == null) return null;


            return new RestaurantEF();
        }
    }
}
