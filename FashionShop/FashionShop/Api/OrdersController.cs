using FashionShop.Models.Domain;
using FashionShop.Models.DTO.OrderDTO;
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
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("get-list-orders")]
        public IActionResult GetAll(int page = 0, int pageSize = 6, int? typePayment = null, int? searchByID = null, string? searchByName = null, string? searchBySDT = null)
        {
            try
            {
                var listOrders = _orderRepository.GetAll(page, pageSize, typePayment, searchByID, searchByName, searchBySDT);            

                return Ok(listOrders);
            }
            catch
            {
                return BadRequest("Lấy danh sách người dùng không thành công");
            }
        }

        [HttpGet("get-totalPayment-by-id/{id}")]
        public IActionResult TotalPayment(int id)
        {
            try
            {
                var totalPayment = _orderRepository.AdminTotalPayment(id);

                return Ok(totalPayment);
            }
            catch
            {
                return BadRequest("Lỗi");
            }
        }

        [HttpGet("get-order-by-id/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _orderRepository.GetById(id);

                if (order != null)
                {
                    return Ok(order);
                }
                else
                {
                    return BadRequest("Không tìm thấy id của order");
                }

            }
            catch
            {
                return BadRequest("Lấy product theo id không thành công");
            }
        }
    }
}
