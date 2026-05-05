namespace MessageProcessor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Using the base class type to handle different subclasses (Polymorphism)
            MessageProcessorone processor;

            Console.WriteLine("--- Processing Approval ---");
            // Execute the fixed algorithm steps for an Approval message
            processor = new ApprovalProcessor();
            processor.Process();

            Console.WriteLine("\n--- Processing Rejection ---");
            // Execute the same sequence, but with different BuildContent implementation
            processor = new RejectionProcessor("Product out of stock");
            processor.Process();

            Console.ReadLine();
        }
    }
}