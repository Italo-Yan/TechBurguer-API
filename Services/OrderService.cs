using System.Security.Cryptography.X509Certificates;
using TechBurguer_API.Entity;
using TechBurguer_API.Enum;
using TechBurguer_API.Models;

namespace TechBurguer_API.Services
{
    public class OrderService : IOrderServices
    {
        private static List<Order> _orders = new List<Order>();

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

        public List<Order> GetAll()
        {
            return _orders;
        }

        public Order? GetById(Guid id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public Order Create(CreateOrder request)
        {
            var newOrder = new Order
            {
                CustomerName = request.CustomerName,
            };
            _orders.Add(newOrder);
            return newOrder;
        }

        public Order? AddItem(Guid orderId, CreateOrderBurger item)
        {
            var order = GetById(orderId);

            if (order == null)
                return null;

            if (order.Status != OrderStatusEnum.Open) 
                throw new Exception("A closed order cannot accept any items.");

            if (!_burgerPrices.TryGetValue(item.Name, out decimal burgerPrice))
                throw new ArgumentException("Burger not found.");

            if (!_drinkPrices.TryGetValue(item.Drink, out decimal drinkPrice))
                throw new ArgumentException("Drink not found.");

            var newItem = new Burger
            {
                Name = item.Name,
                Price = burgerPrice + drinkPrice,
            };

            order.Burgers.Add(newItem);
            return order;
        }

        public bool UpdateStatus(Guid orderId, Enum.OrderStatusEnum status)
        {
            var order = GetById(orderId);

            if (order == null)
                return false;
        
            if (status == OrderStatusEnum.Confirmed && !order.Burgers.Any())
                throw new Exception("An order without items cannot be confirmed.");

            order.Status = status;
            return true;
        }

        public bool Delete(Guid id)
        {
            var order = GetById(id);

            if (order == null)
                return false;

            _orders.Remove(order);
            return true;
        }
    }
}
