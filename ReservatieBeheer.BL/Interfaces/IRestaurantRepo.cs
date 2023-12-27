using ReservatieBeheer.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.BL.Interfaces
{
    public interface IRestaurantRepo
    {
        void VoegRestaurantToe(Restaurant restaurant);
        void VerwijderRestaurant(int restaurantId);
        void UpdateRestaurant(Restaurant restaurant);
        Restaurant GetRestaurantById(int restaurantId);
        IEnumerable<Restaurant> ZoekRestaurants(string postcode, string keuken);
        IEnumerable<(string Naam, string Keuken, Tafel Tafel)> VindGeschikteTafelsPerRestaurant(int aantalPersonen, DateTime gewensteTijd);
    }
}
