namespace PaymentProcessingSystem
{
    public class Order
    {
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }

        public Order(string customerName, decimal amount)
        {
            CustomerName = customerName;
            Amount = amount;
        }
        public override string ToString()
        {
            return $"Order - Customer: {CustomerName}, Amount: {Amount} SAR";
        }
    }
}