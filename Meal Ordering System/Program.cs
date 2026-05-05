using MealOrderingSystem;

//MealDirector → MealBuilder → (fluent methods) → Build() → Meal
// Design Pattern: Builder Pattern
// Display pre-built meals
Console.WriteLine("=== Meal Ordering System ===\n");

var director = new MealDirector();

// Kids Meal
var kidsMeal = director.BuildKidsMeal();
Console.WriteLine($"Kids Meal: {kidsMeal}");
Console.WriteLine($"Price: {kidsMeal.GetPrice():0.00} SAR\n");

// Combo Meal
var comboMeal = director.BuildComboMeal();
Console.WriteLine($"Combo Meal: {comboMeal}");
Console.WriteLine($"Price: {comboMeal.GetPrice():0.00} SAR\n");

// Vegan Meal
var veganMeal = director.BuildVeganMeal();
Console.WriteLine($"Vegan Meal: {veganMeal}");
Console.WriteLine($"Price: {veganMeal.GetPrice():0.00} SAR\n");

// Custom Meal with user preferences
Console.WriteLine("=== Build Your Custom Meal ===");
Console.WriteLine("Available options:");
Console.WriteLine("Main Dishes: Chicken, Beef, Fish, Vegetarian");
Console.WriteLine("Side Dishes: Fries, Rice, Salad, Coleslaw");
Console.WriteLine("Drinks: Coke, Juice, Water, Milkshake");
Console.WriteLine("Desserts: Ice Cream, Cake, Pie, Fruit");

var customMeal = director.BuildCustomMeal();
Console.WriteLine($"\nCustom Meal: {customMeal}");
Console.WriteLine($"Price: {customMeal.GetPrice():0.00} SAR");
