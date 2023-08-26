using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var product = _productRepository.GetAll();
            return View(product);
        }
    }
}
