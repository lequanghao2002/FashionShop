using FashionShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Newtonsoft.Json;

namespace FashionShop.Controllers
{
    public class QuanController : Controller
    {
        public readonly IProductRepository _productRepository;
        public QuanController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult Index(int id)
        {

            var product = _productRepository.GetAll();
            return View(product);
        }

        public IActionResult Detail(int id)
        {
            var product = _productRepository.GetId(id);
            @ViewBag.listImg = JsonConvert.DeserializeObject<List<string>>(product.ListImages); /// ????? WTF
            return View(product);
        }

    }
}
