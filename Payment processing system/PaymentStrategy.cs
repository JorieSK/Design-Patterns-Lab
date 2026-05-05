namespace PaymentProcessingSystem
{
    public interface IPaymentStrategy
    {
        void Pay(decimal amount);
    }
}
