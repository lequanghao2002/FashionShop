using FashionShop.Models;
using FashionShop.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Newtonsoft.Json;

namespace FashionShop.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProductRepository _productRepository;
        public readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index(int idCategory)
        {
            var listProductByCategory = await _productRepository.GetByCategoryId(idCategory);

            ViewBag.NameCategory = _categoryRepository.GetCategoryById(idCategory);

            return View(listProductByCategory);
        }

        public IActionResult Detail(int id)
        {
            var product = _productRepository.GetId(id);
            @ViewBag.listImg = JsonConvert.DeserializeObject<List<string>>(product.ListImages);
            return View(product);
        }

        

    }
}
