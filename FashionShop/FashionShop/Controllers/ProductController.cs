using FashionShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Newtonsoft.Json;

namespace FashionShop.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Detail(int id)
        {
            var product = _productRepository.GetId(id);
            @ViewBag.listImg = JsonConvert.DeserializeObject<List<string>>(product.ListImages);
            return View(product);
        }
        
    }
}
