namespace MealOrderingSystem
{
    public class MealBuilder
    {
        private readonly Meal _meal = new();

        // Fluent API - Builder methods
        public MealBuilder WithMainDish(string mainDish)
        {
            _meal.MainDish = mainDish;
            return this;
        }

        public MealBuilder WithSideDish(string sideDish)
        {
            _meal.SideDish = sideDish;
            return this;
        }

        public MealBuilder WithDrink(string drink)
        {
            _meal.Drink = drink;
            return this;
        }

        public MealBuilder WithDessert(string dessert)
        {
            _meal.Dessert = dessert;
            return this;
        }

        public MealBuilder AsCombo()
        {
            _meal.IsCombo = true;
            return this;
        }

        // Build and return the meal
        public Meal Build()
        {
            return _meal;
        }
    }
}
