using FashionShop.Data;
using FashionShop.Models;
using FashionShop.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FashionShop.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProductRepository _productRepository;
        public readonly ICategoryRepository _categoryRepository;
        public readonly FashionShopDBContext _context;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, FashionShopDBContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _context = context;
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
            var lsprodcut = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CategoryID == product.CategoryID && x.ID != product.ID)
                    .Take(4)
                    .ToList();

            @ViewBag.SanPham = lsprodcut;
            return View(product);
        }
        
    }
}
