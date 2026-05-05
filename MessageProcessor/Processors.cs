using System;

// Concrete Class 1: Handles Approval flow
public class ApprovalProcessor : MessageProcessorone
{
    protected override void BuildContent()
    {
        Console.WriteLine("Content: Your request has been APPROVED!");
    }
}

// Concrete Class 2: Handles Rejection flow with dynamic reasons
public class RejectionProcessor : MessageProcessorone
{
    private readonly string _reason;

    public RejectionProcessor(string reason)
    {
        _reason = reason;
    }

    protected override void BuildContent()
    {
        Console.WriteLine($"Content: Your request was Rejected. Reason: {_reason}");
    }
}