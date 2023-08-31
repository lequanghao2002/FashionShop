using FashionShop.Data;
using FashionShop.Models.Domain;
using FashionShop.Models.DTO;
using FashionShop.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public readonly ICategoryRepository _iCategoryRepository;
        public readonly FashionShopDBContext _identityDbContext;
        public CategoriesController(FashionShopDBContext identityDbContext, ICategoryRepository iCategoryRepository)
        {
            _identityDbContext = identityDbContext;
            _iCategoryRepository = iCategoryRepository;
        }
        [HttpGet("get-all-category")]
        public IActionResult GetAll()
        {
            var allCategorys = _iCategoryRepository.GetAllCategory();
            return Ok(allCategorys);
        }
        [HttpGet]
        [Route("get-category-by-id/{id}")]

        public IActionResult GetCategoryById([FromRoute] int id) 
        {
            var CategoryWithIdDTO = _iCategoryRepository.GetCategoryById(id);
            return Ok(CategoryWithIdDTO);
        }
        [HttpPost("add-category")]
        public IActionResult AddCategory(AddCategoryRequestDTO addCategoryRequestDTO)
        {
            var CategoryAdd = _iCategoryRepository.AddCategory(addCategoryRequestDTO);
            return Ok(CategoryAdd);
        }
        [HttpPut("update-category-by-id/{id}")]
        public IActionResult UpdateCategoryById(int id, [FromBody] AddCategoryRequestDTO CategoryDTO)
        {
            var updateCategory = _iCategoryRepository.UpdateCategoryById(id, CategoryDTO);
            return Ok(updateCategory);
        }
        [HttpDelete("delete-category-by-id/{id}")]
        public IActionResult DeleteCategoryById(int id)
        {
            var deleteCategory = _iCategoryRepository.DeleteCategoryById(id);
            return Ok(deleteCategory);
        }

    }




}
