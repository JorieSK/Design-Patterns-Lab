using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

// Service Lifetimes Explanation (Dependency Injection)
// Singleton: A single instance is created and shared throughout the entire application lifetime.
public class RequestTracker
{
    private int _count = 0;

    public void Track() => _count++;
    public int GetCount() => _count;
}

// Transient: A new instance is created every time it is requested from the service provider.
public class SessionLogger
{
    private readonly List<string> _logs = new();

    public void Log(string message)
    {
        _logs.Add(message);
    }

    public void PrintAll()
    {
        Console.WriteLine("\n===== Session Log =====");
        if (_logs.Count == 0)
        {
            Console.WriteLine("No logs recorded.");
        }
        else
        {
            for (int i = 0; i < _logs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_logs[i]}");
            }
        }
    }
}

// Scoped: A new instance is created once per "scope" (usually a single HTTP request).
// Within the same scope, the same instance is reused.
public class OrderService
{
    private readonly RequestTracker _tracker;
    private readonly SessionLogger _logger;
    public Guid Id { get; } = Guid.NewGuid();

    public OrderService(RequestTracker tracker, SessionLogger logger)
    {
        _tracker = tracker;
        _logger = logger;
    }

    public void PlaceOrder(string item)
    {
        _tracker.Track();
        var message = $"Order placed: {item}";
        _logger.Log(message);
        Console.WriteLine($"New order: {item}");
        Console.WriteLine($"OrderService ID: {Id}");
        Console.WriteLine($"Total requests: {_tracker.GetCount()}");
    }
}

// Transient: Fresh instance is generated every time it's injected/requested.
public class DiscountCalculator
{
    public Guid Id { get; } = Guid.NewGuid();

    public double Calculate(double price)
    {
        Console.WriteLine($"DiscountCalculator ID: {Id}");
        return price * 0.9;
    }
}

// ============================================
// Main Program
// ============================================
class Program
{
    static void Main(string[] args)
    {
        // Initializing the DI Container
        var services = new ServiceCollection();

        // Registering services with their specific lifetimes
        services.AddSingleton<RequestTracker>();    // Shared globally
        services.AddTransient<SessionLogger>();    // New one every time
        services.AddScoped<OrderService>();        // Shared within a scope
        services.AddTransient<DiscountCalculator>(); // New one every time

        var provider = services.BuildServiceProvider();

        Console.WriteLine("===== First Request =====");
        using (var scope1 = provider.CreateScope())
        {
            var order = scope1.ServiceProvider.GetRequiredService<OrderService>();
            var discount = scope1.ServiceProvider.GetRequiredService<DiscountCalculator>();
            var discount2 = scope1.ServiceProvider.GetRequiredService<DiscountCalculator>();

            order.PlaceOrder("Laptop");
            Console.WriteLine($"Price after discount: {discount.Calculate(1000)}");

            // Demonstrating Transient behavior: IDs will be different
            Console.WriteLine($"Same instance? {discount.Id == discount2.Id}"); // False
        }

        Console.WriteLine("\n===== Second Request =====");
        using (var scope2 = provider.CreateScope())
        {
            var order = scope2.ServiceProvider.GetRequiredService<OrderService>();
            var discount = scope2.ServiceProvider.GetRequiredService<DiscountCalculator>();

            order.PlaceOrder("Mobile Phone");
            Console.WriteLine($"Price after discount: {discount.Calculate(500)}");
        }

        // Accessing the logger from the root provider
        var logger = provider.GetRequiredService<SessionLogger>();
        logger.PrintAll();
        Console.WriteLine("Press any key to close...");
        Console.ReadLine();

    }

}