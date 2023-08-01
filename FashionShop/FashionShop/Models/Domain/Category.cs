using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models.Domain
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
