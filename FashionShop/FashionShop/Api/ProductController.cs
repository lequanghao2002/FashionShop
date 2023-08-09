using FashionShop.Data;
using FashionShop.Models.Domain;
using FashionShop.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FashionShop.Api
{
    [Route("api/[controller]")]
    [ApiController]
        public class ProductController : ControllerBase
        {
            private readonly IProductRepository _productRepository;

            public ProductController(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllProducts()
            {
                var products = await _productRepository.GetAllProducts();
                return Ok(products);
            }

            [HttpGet("{productId}")]
            public async Task<IActionResult> GetProductById(int productId)
            {
                var product = await _productRepository.GetProductById(productId);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }

            [HttpGet("category/{categoryId}")]
            public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
            {
                var products = await _productRepository.GetProductsByCategoryId(categoryId);
                return Ok(products);
            }

            [HttpPost]
            public async Task<IActionResult> AddProduct(Product product)
            {
                await _productRepository.AddProduct(product);
                return CreatedAtAction(nameof(GetProductById), new { productId = product.ID }, product);
            }

            [HttpPut("{productId}")]
            public async Task<IActionResult> UpdateProduct(int productId, Product product)
            {
                if (productId != product.ID)
                {
                    return BadRequest();
                }

                await _productRepository.UpdateProduct(product);
                return NoContent();
            }

            [HttpDelete("{productId}")]
            public async Task<IActionResult> DeleteProduct(int productId)
            {
                await _productRepository.DeleteProduct(productId);
                return NoContent();
            }
        }
    }

