using FashionShop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var product = _productRepository.GetAll();

            return View(product);
        }
    }
}
