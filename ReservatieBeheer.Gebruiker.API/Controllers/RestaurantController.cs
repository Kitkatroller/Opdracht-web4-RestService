using Microsoft.AspNetCore.Mvc;

namespace ReservatieBeheer.Gebruiker.API.Controllers
{
    public class RestaurantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
