using System;

public abstract class MessageProcessorone
{
    // Template Method: Defines the skeleton of the algorithm.
    // It is marked as non-virtual (final) to prevent subclasses from overriding the sequence.
    public void Process()
    {
        PrepareGreeting();
        BuildContent();         // Abstract step (Hook/Variable) - Defined by subclasses
        AddOfficialSignature(); 
        Send();               
        LogOperation();         
    }

    // Abstract method: Subclasses must implement their own specific content logic.
    protected abstract void BuildContent();

    // Common/Fixed steps shared by all message types:
    private void PrepareGreeting() => Console.WriteLine("Dear Customer,");

    private void AddOfficialSignature() => Console.WriteLine("\nBest Regards,\nSupport Team");

    private void Send() => Console.WriteLine("Status: Message has been sent.");

    private void LogOperation() => Console.WriteLine("Log: Operation recorded at " + DateTime.Now);
}