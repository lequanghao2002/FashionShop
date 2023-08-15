using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
