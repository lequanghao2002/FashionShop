using FashionShop.Data;
using FashionShop.Models.Domain;
using FashionShop.Models.DTO.OrderDTO;
using FashionShop.Models.DTO.UserDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FashionShop.Repositories
{
    public interface IOrderRepository
    {
        public Task<List<GetOrder>> GetAll(string? searchBySdt);
        
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly FashionShopDBContext _fashionShopDBContext;

        public OrderRepository (FashionShopDBContext fashionShopDBContext)
        {
            _fashionShopDBContext = fashionShopDBContext;
        }


        public async Task<List<GetOrder>> GetAll(string? searchBySdt)
        {
            var listUsersDomain = _fashionShopDBContext.Orders.AsQueryable();

            if (!searchBySdt.IsNullOrEmpty())
            {
                listUsersDomain = listUsersDomain.Where(u => u.PhoneNumber.Contains(searchBySdt));
            }

            var listUsersDTO = await listUsersDomain.Select(order => new GetOrder
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
    }
}
