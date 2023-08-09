using FashionShop.Data;
using FashionShop.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProductById(int productId);
    Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId);
    Task AddProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(int productId);
}

public class ProductRepository : IProductRepository
{
    private List<Product> _products;

    public ProductRepository()
    {
        _products = new List<Product>();
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return _products;
    }

    public async Task<Product> GetProductById(int productId)
    {
        return _products.FirstOrDefault(p => p.ID == productId);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
    {
        return _products.Where(p => p.CatetoryID == categoryId);
    }

    public async Task AddProduct(Product product)
    {
        product.ID = _products.Count + 1;
        _products.Add(product);
    }

    public async Task UpdateProduct(Product product)
    {
        var existingProduct = _products.FirstOrDefault(p => p.ID == product.ID);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
        }
    }

    public async Task DeleteProduct(int productId)
    {
        var productToDelete = _products.FirstOrDefault(p => p.ID == productId);
        if (productToDelete != null)
        {
            _products.Remove(productToDelete);
        }
    }
}

