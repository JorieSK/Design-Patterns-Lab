using System;
using System.Collections.Generic;

/// Singleton Pattern: GameEngine manages the entire game state
/// Only one instance exists throughout the application
public sealed class GameEngine
{
    private static GameEngine _instance;
    private static readonly object _lockObject = new object();

    public int Score { get; set; }
    public int AttemptsRemaining { get; set; }
    public int TimeRemaining { get; set; }
    public bool IsGameActive { get; set; }
    public int RoomNumber { get; private set; }

    private GameEngine()
    {
        Score = 0;
        AttemptsRemaining = 3;
        TimeRemaining = 300; // 5 minutes
        IsGameActive = true;
        RoomNumber = 1;
    }

    /// Thread-safe Singleton accessor
    public static GameEngine Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new GameEngine();
                        DisplayHeader();
                    }
                }
            }
            return _instance;
        }
    }

    private static void DisplayHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔═══════════════════════════════════════════════════╗");
        Console.WriteLine("║       🎮 ESCAPE ROOM BUILDER - GAME ENGINE 🎮  ║");
        Console.WriteLine("║              Singleton Instance: #001            ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    public void DisplayGameStatus()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"╔════════════════════════════════════════╗");
        Console.WriteLine($"║  Room: {RoomNumber,-5} | Score: {Score,-4} | Attempts: {AttemptsRemaining,-2}  ║");
        Console.WriteLine($"║  Time Remaining: {TimeRemaining}s              ║");
        Console.WriteLine($"║  Status: {(IsGameActive ? "ACTIVE" : "INACTIVE"),-28} ║");
        Console.WriteLine($"╚════════════════════════════════════════╝");
        Console.ResetColor();
    }

    public void AddScore(int points)
    {
        Score += points;
    }

    public void DecreaseAttempts()
    {
        AttemptsRemaining--;
        if (AttemptsRemaining <= 0)
        {
            IsGameActive = false;
        }
    }

    public void AdvanceRoom()
    {
        RoomNumber++;
    }

    public void ResetGameState()
    {
        Score = 0;
        AttemptsRemaining = 3;
        TimeRemaining = 300;
        IsGameActive = true;
        RoomNumber = 1;
    }
}
