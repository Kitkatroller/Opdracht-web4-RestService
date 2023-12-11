using Microsoft.AspNetCore.Mvc;

namespace ReservatieBeheer.Beheerder.API.Controllers
{
    public class ReservatieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
