using Microsoft.AspNetCore.Mvc;

namespace ShanesRestaurantApp.Models
{
    public class WebAPIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
