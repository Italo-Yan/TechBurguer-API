using TechBurguer_API.Entity;
using TechBurguer_API.Models;

namespace TechBurguer_API.Services
{
    public interface IOrderServices
    {
        List<Order> GetAll();
        Order? GetById(Guid id);
        Order Create(CreateOrder request);
        Order? AddItem(Guid orderId, CreateOrderBurger item);
        bool UpdateStatus(Guid orderId, Enum.OrderStatusEnum status);
        bool Delete(Guid id);
    }
}
