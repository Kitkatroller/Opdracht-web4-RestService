using Microsoft.AspNetCore.Mvc;

namespace ReservatieBeheer.Beheerder.API.Controllers
{
    public class GebruikerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
