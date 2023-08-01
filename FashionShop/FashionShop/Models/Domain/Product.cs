using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShop.Models.Domain
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CatetoryID { get; set; }
        public int Quantity { get; set; }
        public string Describe { get; set; }
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string ListImage { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime UpdatedBy { get; set; }
        public bool Status { get; set; }
    }
}
