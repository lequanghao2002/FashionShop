using FashionShop.Models.Domain;

namespace FashionShop.Models.DTO.OrderDTO
{
    public class GetOrderByUserIdDTO
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }
        public int WardID { get; set; }
        public string Address { get; set; }
        public double DeliveryFee { get; set; }
        public string? Note { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public string UserID { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public Voucher? Voucher { get; set; }
    }
}
