using TechBurguer_API.Enum;

namespace TechBurguer_API.Entity
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public OrderStatusEnum Status { get; set; }
        public List<Burger> Burgers { get; set; }

        public decimal TotalPrice
        {
            get { return Burgers.Sum(b => b.Price); }
        }

        public Order()
        {
            Id = Guid.NewGuid();
            Status = OrderStatusEnum.Open;
            Burgers = new List<Burger>();

        }
    }
}
