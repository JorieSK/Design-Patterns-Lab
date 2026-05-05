namespace PaymentProcessingSystem
{
    public class CreditCardPayment : IPaymentStrategy
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine("[Credit Card] Charged SAR " + amount.ToString("F2") + " + 2.5% fee");
        }
    }

    public class CashPayment : IPaymentStrategy
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine("[Cash] Charged SAR " + amount.ToString("F2"));
        }
    }

    public class ApplePayPayment : IPaymentStrategy
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine("[Apple Pay] Charged SAR " + amount.ToString("F2"));
        }
    }

    public class STCPayPayment : IPaymentStrategy
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine("[STC Pay] Charged SAR " + amount.ToString("F2"));
        }
    }
}
