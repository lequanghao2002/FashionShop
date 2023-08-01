namespace FashionShop.Models.Domain
{
    public class Order
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; }
        public string UserID { get; set; }
    }
}
