using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;
using System.Text.Json;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly RabbitMQPublisher _publisher;

        public OrdersController()
        {
            _publisher = new RabbitMQPublisher("localhost", "orders-queue");
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            var message = JsonSerializer.Serialize(order);

            _publisher.Publish(message);

            return Ok("Order created and event published.");
        }
    }
}