using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models.Domain
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
    }
}
