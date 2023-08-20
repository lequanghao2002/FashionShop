using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShop.Models.Domain
{
    [Table("Vouchers")]
    public class Voucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column("varchar")]
        public string DiscountCode { get; set; }

        [Required]
        public string DiscountType { get; set; }

        [Required]
        public double DiscountValue { get; set; }

        public double MinimumValue { get; set;}

        [Required]
        public int Quantity { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string? Describe { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

    }
}
