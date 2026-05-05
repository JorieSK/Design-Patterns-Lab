using System;

/// Template Method Pattern: Defines the skeleton of puzzle execution
/// Subclasses implement specific puzzle logic while maintaining consistent flow
public abstract class Puzzle
{
    protected string _question;
    protected string _correctAnswer;
    protected int _pointsReward;

    protected Puzzle(string question, string correctAnswer, int pointsReward)
    {
        _question = question;
        _correctAnswer = correctAnswer;
        _pointsReward = pointsReward;
    }

    /// Template Method: Defines the algorithm structure
    public bool Execute()
    {
        DisplayHeader();
        DisplayQuestion();
        string playerAnswer = GetPlayerInput();
        bool isCorrect = ValidateInput(playerAnswer);
        DisplayResult(isCorrect);

        if (isCorrect)
        {
            GameEngine.Instance.AddScore(_pointsReward);
        }
        else
        {
            GameEngine.Instance.DecreaseAttempts();
        }

        return isCorrect;
    }

    /// <summary>
    /// Template Step 1: Display puzzle header
    /// </summary>
    protected virtual void DisplayHeader()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n┌─────────────────────────────────────┐");
        Console.WriteLine($"│  PUZZLE: {GetType().Name,-25}        │");
        Console.WriteLine("└─────────────────────────────────────┘");
        Console.ResetColor();
    }

    /// Template Step 2: Display the question
    protected virtual void DisplayQuestion()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n❓ {_question}");
        Console.ResetColor();
    }

    /// Template Step 3: Get player input
    protected virtual string GetPlayerInput()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("➤ Your answer: ");
        Console.ResetColor();
        return Console.ReadLine() ?? string.Empty;
    }

    /// Template Step 4: Validate input (can be overridden for custom validation)
    protected virtual bool ValidateInput(string playerAnswer)
    {
        return playerAnswer.Equals(_correctAnswer, StringComparison.OrdinalIgnoreCase);
    }

    /// Template Step 5: Display result
    protected virtual void DisplayResult(bool isCorrect)
    {
        if (isCorrect)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Correct! Door Unlocked. +{0} Points!\n", _pointsReward);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("✗ Wrong Answer! Attempts remaining: {0}\n", GameEngine.Instance.AttemptsRemaining);
        }
        Console.ResetColor();
    }

    public abstract string GetPuzzleType();
}
