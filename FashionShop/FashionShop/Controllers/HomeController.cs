using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
