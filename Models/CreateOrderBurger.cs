using TechBurguer_API.Enum;

namespace TechBurguer_API.Models
{
    public class CreateOrderBurger
    {
        public string Name { get; set; }
        public DrinkEnum Drink { get; set; }
    }
}
