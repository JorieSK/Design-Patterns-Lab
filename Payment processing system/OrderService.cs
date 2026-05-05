namespace PaymentProcessingSystem
{
    /* ============ PREVIOUS IMPLEMENTATION (Before Strategy Pattern) ============
    public class OrderService
    {
        public void Checkout(Order order, string paymentType)
        {
            if (paymentType.ToLower() == "card")
            {
                Console.WriteLine($"Processing credit/debit card payment for {order.CustomerName}");
                Console.WriteLine($"Amount: {order.Amount} SAR");
                Console.WriteLine("Payment processed successfully via Card.");
                Console.WriteLine();
            }
            else if (paymentType.ToLower() == "cash")
            {
                Console.WriteLine($"Processing cash payment for {order.CustomerName}");
                Console.WriteLine($"Amount: {order.Amount} SAR");
                Console.WriteLine("Payment processed successfully via Cash.");
                Console.WriteLine();
            }
            else if (paymentType.ToLower() == "applepay")
            {
                Console.WriteLine($"Processing Apple Pay payment for {order.CustomerName}");
                Console.WriteLine($"Amount: {order.Amount} SAR");
                Console.WriteLine("Payment processed successfully via Apple Pay.");
                Console.WriteLine();
            }
            else if (paymentType.ToLower() == "stcpay")
            {
                Console.WriteLine($"Processing STC Pay payment for {order.CustomerName}");
                Console.WriteLine($"Amount: {order.Amount} SAR");
                Console.WriteLine("Payment processed successfully via STC Pay.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Error: Payment type '{paymentType}' is not supported.");
                Console.WriteLine();
            }
        }
    }
    ============ END OF PREVIOUS IMPLEMENTATION ============ */


    // ============ NEW IMPLEMENTATION (Strategy Pattern) ============
    public class OrderServices
    {
        private IPaymentStrategy? _strategy;
        public void SetStrategy(IPaymentStrategy s) => _strategy = s;
        public void Checkout(decimal amount) => _strategy?.Pay(amount);
    }
}
