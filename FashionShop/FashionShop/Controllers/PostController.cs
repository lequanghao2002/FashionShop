using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
