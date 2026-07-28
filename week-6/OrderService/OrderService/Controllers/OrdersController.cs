using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderDbContext _context;
        private readonly HttpClient _httpClient;

        public OrdersController(OrderDbContext context, IHttpClientFactory factory)
        {
            _context = context;
            _httpClient = factory.CreateClient();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(Order order)
        {
            var response = await _httpClient.GetAsync(
                $"https://localhost:7214/api/Products/{order.ProductId}");

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Product not found.");
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }
    }
}