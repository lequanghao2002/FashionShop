using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShop.Models.Domain
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage ="vui long nhap so dien thoai")]
        [Display(Name ="dien thoai")]
        [DataType(DataType.PhoneNumber)]
        [Remote(action:"checkPhone", controller: "Contact")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Content { get; set; }
        public bool Status { get; set; }
    }
}
