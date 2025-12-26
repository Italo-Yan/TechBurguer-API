using Microsoft.AspNetCore.Mvc;
using System;
using TechBurguer_API.Entity;
using TechBurguer_API.Enum;
using TechBurguer_API.Models;

namespace TechBurguer_API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private static readonly Dictionary<string, decimal> _burgerPrices = new()
        {
            { "Cheeseburger", 25.00m },
            { "X-Burger", 20.00m },
            { "X-Egg", 30.00m },
            { "X-Dog", 35.00m },
            { "X-Full", 40.00m }
        };

        private static readonly Dictionary<DrinkEnum, decimal> _drinkPrices = new()
        {
            { DrinkEnum.CocaCola, 8.00m },
            { DrinkEnum.Pepsi, 8.50m },
            { DrinkEnum.Fanta, 7.50m },
            { DrinkEnum.Sprite, 7.50m },
            { DrinkEnum.Water, 5.00m }
        };

        private static List<Order> _orders = new List<Order>();

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrder request)
        {
            if (string.IsNullOrEmpty(request.CustomerName) || request.CustomerName.Length < 3)
                return BadRequest("Customer name must be at least 3 characters long.");

            var newOrder = new Order
            {
                CustomerName = request.CustomerName,
            };

            _orders.Add(newOrder);

            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.Id }, newOrder);
        }

        [HttpPost("{id}/items")]
        public IActionResult AddItemToOrder(Guid id, [FromBody] CreateOrderBurger request)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound("Order not found!");

            if (order.Status != OrderStatusEnum.Open)
                return BadRequest("Cannot add items to a closed/confirmed order.");

            if (!_burgerPrices.TryGetValue(request.Name, out decimal burgerPrice))
                return BadRequest($"Burger '{request.Name}' is not on the menu!");

            if (!_drinkPrices.TryGetValue(request.Drink, out decimal drinkPrice))
                return BadRequest($"Drink '{request.Drink}' is not on the menu!");

            var newItem = new Burger
            {
                Name = request.Name,
                Price = burgerPrice + drinkPrice
            };

            order.Burgers.Add(newItem);

            return Ok(order);
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Ok(_orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(Guid id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound("Order not found!");
            return Ok(order);
        }

        [HttpPatch("{id}/status")]
        public IActionResult UpdateOrderStatus(Guid id, [FromBody] EditOrder request)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound("Order not found!");

            if (request.Status == OrderStatusEnum.Confirmed && !order.Burgers.Any())
            {
                return BadRequest("Cannot confirm an empty order. Please add items first.");
            }

            order.Status = request.Status;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(Guid id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound("Order not found!");

            _orders.Remove(order);
            return NoContent();
        }
    }
}