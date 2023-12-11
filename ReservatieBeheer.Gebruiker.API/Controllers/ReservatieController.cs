using Microsoft.AspNetCore.Mvc;

namespace ReservatieBeheer.Gebruiker.API.Controllers
{
    public class ReservatieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
