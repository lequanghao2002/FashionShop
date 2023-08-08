using FashionShop.Models.Domain;
using FashionShop.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        //private readonly Order _order;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            //_order = order;
            _orderRepository = orderRepository;
        }
        [HttpGet("get-order")]
        public async Task<IActionResult> GetListUsers(string? searchBySdt)
        {
            try
            {
                var listUsers = await _orderRepository.GetAll(searchBySdt);

                return Ok(listUsers);
            }
            catch
            {
                return BadRequest("Lấy danh sách người dùng không thành công");
            }
        }
    }
}
