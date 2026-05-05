using PaymentProcessingSystem;

/* ============ PREVIOUS IMPLEMENTATION (Before Strategy Pattern) ============
// Create OrderService
OrderService orderService = new OrderService();

// Create 3 test orders
Order order1 = new Order("Ahmad", 150.00m);
Order order2 = new Order("Fatima", 250.50m);
Order order3 = new Order("Mohammed", 75.99m);

// Test all payment methods
Console.WriteLine("===== Payment Processing System =====\n");

Console.WriteLine("--- Order 1: Payment via Card ---");
orderService.Checkout(order1, "card");

Console.WriteLine("--- Order 2: Payment via Cash ---");
orderService.Checkout(order2, "cash");

Console.WriteLine("--- Order 3: Payment via Apple Pay ---");
orderService.Checkout(order3, "applepay");

Console.WriteLine("--- Order 1: Payment via STC Pay ---");
orderService.Checkout(order1, "stcpay");

Console.WriteLine("--- Order 2: Payment via STC Pay ---");
orderService.Checkout(order2, "stcpay");

Console.WriteLine("
--- Order 3: Payment via STC Pay ---");
orderService.Checkout(order3, "stcpay");
============ END OF PREVIOUS IMPLEMENTATION ============ */


// ============ NEW IMPLEMENTATION (Strategy Pattern) ============

var service = new OrderServices();
    service.SetStrategy(new CreditCardPayment());
    service.Checkout(150.00m);

    Console.WriteLine("Press any key to close...");
    Console.ReadLine();



