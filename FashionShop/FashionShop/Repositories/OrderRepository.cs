using FashionShop.Data;
using FashionShop.Models.Domain;
using FashionShop.Models.DTO.OrderDTO;
using FashionShop.Models.DTO.UserDTO;
using FashionShop.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

namespace FashionShop.Repositories
{
    public interface IOrderRepository
    {
        public Task<List<GetOrderDTO>> GetAll(string? searchBySdt);
        public Task<GetOrderByUserIdDTO> GetById(int id);
        public Task<List<GetOrderByUserIdDTO>> GetByUserID(string userID);
        public Task<GetOrderByUserIdDTO> GetNewByUserID(string userID);
        public Task<GetOrderDTO> Create(CreateOrderDTO createOrderDTO);
        public Task<List<ShoppingCartViewModel>> Cancel(int id);
        public double TotalPayment(GetOrderByUserIdDTO getOrderByUserIdDTO);
        public bool PayOnline(int id);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly FashionShopDBContext _fashionShopDBContext;

        public OrderRepository (FashionShopDBContext fashionShopDBContext)
        {
            _fashionShopDBContext = fashionShopDBContext;
        }

        public async Task<List<GetOrderDTO>> GetAll(string? searchBySdt)
        {
            var listUsersDomain = _fashionShopDBContext.Orders.AsQueryable();

            if (!searchBySdt.IsNullOrEmpty())
            {
                listUsersDomain = listUsersDomain.Where(u => u.PhoneNumber.Contains(searchBySdt));
            }

            var listUsersDTO = await listUsersDomain.Select(order => new GetOrderDTO
            {
                ID = order.ID,
                FullName = order.FullName,
                Email = order.Email,
                PhoneNumber = order.PhoneNumber,
                Address = order.Address,
                OrderDate = order.OrderDate,
                Note = order.Note,
                
            }).OrderByDescending(u => u.ID).ToListAsync();

            return listUsersDTO;
        }

        public async Task<List<GetOrderByUserIdDTO>> GetByUserID(string userID)
        {
            var orderByUserIdDTO = await _fashionShopDBContext.Orders.Select(order => new GetOrderByUserIdDTO
            {
                ID = order.ID,
                FullName = order.FullName,
                PhoneNumber = order.PhoneNumber,
                ProvinceName = order.Province.Name,
                DistrictName = order.District.Name,
                WardName = order.Ward.Name,
                Address = order.Address,
                DeliveryFee = order.DeliveryFee,
                OrderDate = order.OrderDate,
                Note = order.Note,
                Status = order.Status,

                Voucher = order.Voucher,
                UserID = order.UserID,

                OrderDetails = order.OrderDetails.ToList()
            }).Where(order => order.UserID == userID).OrderByDescending(o => o.OrderDate).ToListAsync();

            
            return orderByUserIdDTO;
           
        }

        public async Task<GetOrderByUserIdDTO> GetById(int id)
        {
            var orderByIdDTO = await _fashionShopDBContext.Orders.Select(order => new GetOrderByUserIdDTO
            {
                ID = order.ID,
                Email = order.Email,
                FullName = order.FullName,
                PhoneNumber = order.PhoneNumber,
                ProvinceName = order.Province.Name,
                DistrictName = order.District.Name,
                WardName = order.Ward.Name,
                Address = order.Address,
                DeliveryFee = order.DeliveryFee,
                OrderDate = order.OrderDate,
                Note = order.Note,
                Status = order.Status,

                Voucher = order.Voucher,
                UserID = order.UserID,

                OrderDetails = order.OrderDetails.Select(od => new OrderDetail()
                {
                    ProductID = od.ProductID,
                    Product = _fashionShopDBContext.Products.SingleOrDefault(p => p.ID == od.ProductID),
                    OrderID = od.OrderID,
                    Price = od.Price,
                    Quantity = od.Quantity

                }).ToList(),
            }).SingleOrDefaultAsync(o => o.ID == id);

            return orderByIdDTO;
        }

        public async Task<GetOrderByUserIdDTO> GetNewByUserID(string id)
        {
            var orderByUserIdDTO = await _fashionShopDBContext.Orders.Select(order => new GetOrderByUserIdDTO
            {
                ID = order.ID,
                Email = order.Email,
                FullName = order.FullName,
                PhoneNumber = order.PhoneNumber,
                ProvinceName = order.Province.Name,
                DistrictName = order.District.Name,
                WardName = order.Ward.Name,
                Address = order.Address,
                DeliveryFee = order.DeliveryFee,
                OrderDate = order.OrderDate,
                Note = order.Note,
                Status = order.Status,

                Voucher = order.Voucher,
                UserID = order.UserID,

                OrderDetails = order.OrderDetails.Select(od => new OrderDetail()
                {
                    ProductID = od.ProductID,
                    Product = _fashionShopDBContext.Products.SingleOrDefault(p => p.ID == od.ProductID),
                    OrderID = od.OrderID,
                    Price = od.Price,
                    Quantity = od.Quantity

                }).ToList(),
            }).Where(o => o.UserID == id).OrderByDescending(o => o.OrderDate).FirstOrDefaultAsync();

            return orderByUserIdDTO;
        }

        public async Task<GetOrderDTO> Create(CreateOrderDTO createOrderDTO)
        {
            var orderDomain = new Order()
            {
                FullName = createOrderDTO.FullName,
                Email = createOrderDTO.Email,
                PhoneNumber = createOrderDTO.PhoneNumber,
                ProvinceID = createOrderDTO.ProvinceID,
                DistrictID = createOrderDTO.DistrictID,
                WardID = createOrderDTO.WardID,
                Address = createOrderDTO.Address,
                Note = createOrderDTO.Note,
                DeliveryFee = createOrderDTO.DeliveryFee,
                TypePayment = createOrderDTO.TypePayment,
                OrderDate = DateTime.Now,
                Status = 1,

                UserID = createOrderDTO.UserID,
                VoucherID = createOrderDTO.VoucherID,
            };
            await _fashionShopDBContext.Orders.AddAsync(orderDomain);
            await _fashionShopDBContext.SaveChangesAsync();

            foreach (var item in createOrderDTO.shoppingCarts)
            {
                var orderDetailDomain = new OrderDetail()
                {
                    OrderID = orderDomain.ID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                };

                if (item.Product.Discount > 0)
                {
                    orderDetailDomain.Price = item.Product.Price - (item.Product.Price * item.Product.Discount / 100);
                }
                else
                {
                    orderDetailDomain.Price = item.Product.Price;
                }

                await _fashionShopDBContext.OrderDetails.AddAsync(orderDetailDomain);
                await _fashionShopDBContext.SaveChangesAsync();
            }

            var orderNew = new GetOrderDTO()
            {
                ID = orderDomain.ID,
                FullName = createOrderDTO.FullName,
                Email = createOrderDTO.Email,
                PhoneNumber = createOrderDTO.PhoneNumber,
                ProvinceID = createOrderDTO.ProvinceID,
                DistrictID = createOrderDTO.DistrictID,
                WardID = createOrderDTO.WardID,
                Address = createOrderDTO.Address,
                Note = createOrderDTO.Note,
                DeliveryFee = createOrderDTO.DeliveryFee,
                OrderDate = DateTime.Now,
                TypePayment = createOrderDTO.TypePayment,
                TypePaymentVN = createOrderDTO.TypePaymentVN,
                shoppingCarts = createOrderDTO.shoppingCarts,

                UserID = createOrderDTO.UserID,
                VoucherID = createOrderDTO.VoucherID,
            };
                
            return orderNew;
        }

        public async Task<List<ShoppingCartViewModel>> Cancel(int id)
        {
            var orderById = await _fashionShopDBContext.Orders.SingleOrDefaultAsync(o => o.ID == id);
            
            if(orderById != null)
            {
                orderById.Status = 0;
                await _fashionShopDBContext.SaveChangesAsync();

                // Tăng số lượng voucher khi hủy hàng
                if (orderById.VoucherID != null)
                {
                    var voucher = await _fashionShopDBContext.Vouchers.SingleOrDefaultAsync(v => v.ID == orderById.VoucherID);

                    voucher!.Quantity += 1;
                    await _fashionShopDBContext.SaveChangesAsync();
                }

                var listOrder = await _fashionShopDBContext.OrderDetails.Where(od => od.OrderID == orderById.ID).Select(o => new ShoppingCartViewModel
                {
                    ProductID = o.ProductID,
                    Quantity = o.Quantity,
                }).ToListAsync();

                return listOrder;
            }

            return null;
        }

        public double TotalPayment(GetOrderByUserIdDTO getOrderByUserIdDTO)
        {
            double totalMoney = 0;

            foreach (var item in getOrderByUserIdDTO.OrderDetails)
            {
                var quantity = item.Quantity;
                if (item.Product.Discount > 0)
                {
                    var price = item.Product.Price - (item.Product.Price * item.Product.Discount / 100);
                    totalMoney += price * quantity;
                }
                else
                {
                    var price = item.Product.Price;
                    totalMoney += price * quantity;
                }
            }

            double voucherValue = 0;

            if (getOrderByUserIdDTO.Voucher != null)
            {
                if (getOrderByUserIdDTO.Voucher.DiscountAmount == true)
                {
                    voucherValue = getOrderByUserIdDTO.Voucher.DiscountValue;
                }
                else
                {
                    voucherValue = totalMoney * getOrderByUserIdDTO.Voucher.DiscountValue / 100;
                }
            }

            double totalPayment = totalMoney + getOrderByUserIdDTO.DeliveryFee - voucherValue;

            return totalPayment;
        }

        public bool PayOnline(int id)
        {
            var orderById = _fashionShopDBContext.Orders.SingleOrDefault(o => o.ID == id);

            if(orderById != null)
            {
                orderById.Status = 3;
                _fashionShopDBContext.SaveChanges();
                return true;
            }

            return false;
        }

    }
}
