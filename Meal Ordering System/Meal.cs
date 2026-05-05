namespace MealOrderingSystem
{
    public class Meal
    {
        public string MainDish { get; set; } = "None";
        public string SideDish { get; set; } = "None";
        public string Drink { get; set; } = "None";
        public string Dessert { get; set; } = "None";
        public bool IsCombo { get; set; } = false;

        public static readonly Dictionary<string, decimal> MainDishPrices = new()
        {
            { "Chicken", 22.50m },
            { "Beef", 30.00m },
            { "Fish", 26.25m },
            { "Vegetarian", 18.75m },
            { "None", 0m }
        };

        public static readonly Dictionary<string, decimal> SideDishPrices = new()
        {
            { "Fries", 7.50m },
            { "Rice", 5.60m },
            { "Salad", 9.35m },
            { "Coleslaw", 7.50m },
            { "None", 0m }
        };

        public static readonly Dictionary<string, decimal> DrinkPrices = new()
        {
            { "Coke", 9.35m },
            { "Juice", 9.35m },
            { "Water", 5.60m },
            { "Milkshake", 13.10m },
            { "None", 0m }
        };

        public static readonly Dictionary<string, decimal> DessertPrices = new()
        {
            { "Ice Cream", 11.25m },
            { "Cake", 13.10m },
            { "Pie", 15.00m },
            { "Fruit", 7.50m },
            { "None", 0m }
        };

        // Calculate price based on components
        public decimal GetPrice()
        {
            decimal price = 0;

            price += MainDishPrices.GetValueOrDefault(MainDish, 0);
            price += SideDishPrices.GetValueOrDefault(SideDish, 0);
            price += DrinkPrices.GetValueOrDefault(Drink, 0);
            price += DessertPrices.GetValueOrDefault(Dessert, 0);

            // Apply combo discount if it's a combo
            if (IsCombo && MainDish != "None" && SideDish != "None" && Drink != "None")
            {
                price *= 0.85m; // 15% discount
            }

            return price;
        }

        public override string ToString()
        {
            var parts = new List<string>();
            if (MainDish != "None") parts.Add($"Main: {MainDish}");
            if (SideDish != "None") parts.Add($"Side: {SideDish}");
            if (Drink != "None") parts.Add($"Drink: {Drink}");
            if (Dessert != "None") parts.Add($"Dessert: {Dessert}");

            string result = string.Join(", ", parts);
            if (IsCombo) result += " [COMBO]";
            return result;
        }
    }
}
