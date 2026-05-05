namespace MealOrderingSystem
{
    // Design Pattern: Builder Pattern
    // Role: Director Class
    // Purpose: Orchestrates the construction steps using a Builder instance. 
    // It defines "recipes" for specific configurations (KidsMeal, VeganMeal, etc.).
    public class MealDirector
    {
        // Pre-defined recipe for a Kids Meal
        public Meal BuildKidsMeal()
        {
            return new MealBuilder()
                .WithMainDish("Chicken")
                .WithSideDish("Fries")
                .WithDrink("Juice")
                .WithDessert("Ice Cream")
                .AsCombo()
                .Build();
        }

        // Pre-defined recipe for a Standard Combo
        public Meal BuildComboMeal()
        {
            return new MealBuilder()
                .WithMainDish("Beef")
                .WithSideDish("Fries")
                .WithDrink("Coke")
                .WithDessert("Pie")
                .AsCombo()
                .Build();
        }

        // Pre-defined recipe for a Vegan option
        public Meal BuildVeganMeal()
        {
            return new MealBuilder()
                .WithMainDish("Vegetarian")
                .WithSideDish("Salad")
                .WithDrink("Water")
                .WithDessert("Fruit")
                .AsCombo()
                .Build();
        }

        // Flexible construction: Builds a custom Meal based on dynamic user input
        public Meal BuildCustomMeal()
        {
            var builder = new MealBuilder();

            // Step-by-step assembly based on user preferences
            Console.Write("\nSelect Main Dish (Chicken/Beef/Fish/Vegetarian): ");
            var mainDish = Console.ReadLine() ?? "None";
            if (IsValidMainDish(mainDish))
                builder.WithMainDish(CapitalizeFirstLetter(mainDish));

            Console.Write("Select Side Dish (Fries/Rice/Salad/Coleslaw): ");
            var sideDish = Console.ReadLine() ?? "None";
            if (IsValidSideDish(sideDish))
                builder.WithSideDish(CapitalizeFirstLetter(sideDish));

            Console.Write("Select Drink (Coke/Juice/Water/Milkshake): ");
            var drink = Console.ReadLine() ?? "None";
            if (IsValidDrink(drink))
                builder.WithDrink(CapitalizeFirstLetter(drink));

            Console.Write("Select Dessert (Ice Cream/Cake/Pie/Fruit): ");
            var dessert = Console.ReadLine() ?? "None";
            if (IsValidDessert(dessert))
                builder.WithDessert(CapitalizeFirstLetter(dessert));

            Console.Write("Make it a combo for 15% discount? (yes/no): ");
            var isCombo = Console.ReadLine()?.ToLower() == "yes";
            if (isCombo)
                builder.AsCombo();

            // Final step: Return the fully constructed Product
            return builder.Build();
        }

        // Helper methods for validation and formatting
        private string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        private bool IsValidMainDish(string dish)
        {
            return dish.Equals("Chicken", StringComparison.OrdinalIgnoreCase) ||
                   dish.Equals("Beef", StringComparison.OrdinalIgnoreCase) ||
                   dish.Equals("Fish", StringComparison.OrdinalIgnoreCase) ||
                   dish.Equals("Vegetarian", StringComparison.OrdinalIgnoreCase);
        }   

        private bool IsValidSideDish(string dish) =>
            dish.Equals("Fries", StringComparison.OrdinalIgnoreCase) ||
            dish.Equals("Rice", StringComparison.OrdinalIgnoreCase) ||
            dish.Equals("Salad", StringComparison.OrdinalIgnoreCase) ||
            dish.Equals("Coleslaw", StringComparison.OrdinalIgnoreCase);

        private bool IsValidDrink(string drink) =>
            drink.Equals("Coke", StringComparison.OrdinalIgnoreCase) ||
            drink.Equals("Juice", StringComparison.OrdinalIgnoreCase) ||
            drink.Equals("Water", StringComparison.OrdinalIgnoreCase) ||
            drink.Equals("Milkshake", StringComparison.OrdinalIgnoreCase);

        private bool IsValidDessert(string dessert) =>
            dessert.Equals("Ice Cream", StringComparison.OrdinalIgnoreCase) ||
            dessert.Equals("Cake", StringComparison.OrdinalIgnoreCase) ||
            dessert.Equals("Pie", StringComparison.OrdinalIgnoreCase) ||
            dessert.Equals("Fruit", StringComparison.OrdinalIgnoreCase);
    }
}