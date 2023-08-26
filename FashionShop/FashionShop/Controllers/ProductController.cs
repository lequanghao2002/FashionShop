using FashionShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace FashionShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
        
    }
}
