using Microsoft.AspNetCore.Mvc;
using TechBurguer_API.Models;
using TechBurguer_API.Services;

namespace TechBurguer_API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _service;

        public OrderController(IOrderServices service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrder request)
        {
            var order = _service.Create(request);

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [HttpPost("{id}/items")]
        public IActionResult AddItemToOrder(Guid id, [FromBody] CreateOrderBurger request)
        {
            try
            {
                var order = _service.AddItem(id, request);
                if (order == null) 
                    return NotFound();

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(Guid id)
        {
            var order = _service.GetById(id);

            if (order == null)
                return NotFound("Order not found");
            return Ok(order);
        }

        [HttpPatch("{id}/status")]
        public IActionResult UpdateOrderStatus(Guid id, [FromBody] EditOrder request)
        {
           try
            {
                var sucess = _service.UpdateStatus(id, request.Status);
                if (!sucess)
                    return NotFound("Order not found!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(Guid id)
        {
            var sucess = _service.Delete(id);
            if (!sucess)
                return NotFound("Order not found!");
            return NoContent();
        }
    }
}