namespace FashionShop.Models.Domain
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public bool Price { get; set; }
        public int Quantity { get; set; }
    }
}
