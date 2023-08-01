using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShop.Models.Domain
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public int CatetoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }  

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Describe { get; set; }

        [Required]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string ListImages { get; set; }

        [Required]
        public double Price { get; set; }

        public double Discount { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime UpdatedBy { get; set; }

        public bool Status { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
