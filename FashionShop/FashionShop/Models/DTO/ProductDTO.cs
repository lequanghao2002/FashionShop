using FashionShop.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Models.DTO
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class ProductByIdDTO
    {
        public string Name { get; set; }
    }
    public class AddProductRequestDTO
    {
        public string Name { get; set; }
    }
}

