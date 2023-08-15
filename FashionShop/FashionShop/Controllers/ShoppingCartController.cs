using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
