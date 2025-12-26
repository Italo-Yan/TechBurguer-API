using TechBurguer_API.Enum;

namespace TechBurguer_API.Entity
{
    public class Burger
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DrinkEnum Drink { get; set; }

        public Burger()
        {
            Id = Guid.NewGuid();
        }
    }
}
