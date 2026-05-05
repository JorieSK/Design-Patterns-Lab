using System;
using System.Linq;

/// MathPuzzle: Concrete implementation of the Template Method
/// Focuses on mathematical problem solving
public class MathPuzzle : Puzzle
{
    public MathPuzzle() 
        : base("Solve: 5 + (2 * 3) = ?", "11", 10)
    {
    }

    public override string GetPuzzleType() => "Mathematics";

    protected override bool ValidateInput(string playerAnswer)
    {
        // Math puzzles need numeric validation
        if (int.TryParse(playerAnswer, out int answer))
        {
            return answer == int.Parse(_correctAnswer);
        }
        return false;
    }
}

/// PasswordPuzzle: Text-based puzzle
public class PasswordPuzzle : Puzzle
{
    public PasswordPuzzle()
     : base("I am not just code; I think, I act, and I observe. I use tools to reach my goal autonomously. What am I in the world of modern AI?", "AGENT", 20)
    {
    }

    public override string GetPuzzleType() => "Password";
}

/// LogicPuzzle: Requires logical deduction
public class LogicPuzzle : Puzzle
{
    public LogicPuzzle() 
        : base(" I am unique. I have only one instance, and everyone knows where to find me. What pattern am I?", "SINGLETON", 20)
    {
    }

    public override string GetPuzzleType() => "Logic Riddle";
}

/// CodePuzzle: Binary/Hex puzzle
public class CodePuzzle : Puzzle
{
    public CodePuzzle() 
        : base("Find the hidden name(First letter of each word): Trust United Wisdom Always Inspires Quality", "TUWAIQ", 25)
    {
    }

    public override string GetPuzzleType() => "Code Breaking";
}
