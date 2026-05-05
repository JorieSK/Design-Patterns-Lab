using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Room class: Represents an escape room with puzzles and traps
/// </summary>
public class Room
{
    public string Name { get; set; }
    public string Difficulty { get; set; }
    public int TimeLimit { get; set; }
    public List<Puzzle> Puzzles { get; set; }
    public List<string> Traps { get; set; }
    public string Description { get; set; }

    public Room()
    {
        Puzzles = new List<Puzzle>();
        Traps = new List<string>();
    }

    public void DisplayRoomInfo()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n╔════════════════════════════════════════╗");
        Console.WriteLine($"║  🏚️  ROOM: {Name,-25}  🏚️  ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n📝 {Description}");
        Console.WriteLine($"⚡ Difficulty: {Difficulty}");
        Console.WriteLine($"⏱️  Time Limit: {TimeLimit}s");
        Console.WriteLine($"🔐 Puzzles: {Puzzles.Count}");
        Console.WriteLine($"⚠️  Traps: {string.Join(", ", Traps.Count > 0 ? Traps : new List<string> { "None" })}");
        Console.ResetColor();
        Console.WriteLine();
    }
}

/// <summary>
/// Builder Pattern: Constructs a Room with fluent interface
/// Allows flexible room configuration without modifying constructor
/// </summary>
public class RoomBuilder
{
    private Room _room;

    public RoomBuilder()
    {
        Reset();
    }

    public RoomBuilder Reset()
    {
        _room = new Room();
        _room.Name = "Unknown Chamber";
        _room.Difficulty = "Normal";
        _room.TimeLimit = 300;
        _room.Description = "A mysterious room awaits you.";
        return this;
    }

    public RoomBuilder WithName(string name)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  ✓ Setting room name: '{name}'");
        Console.ResetColor();
        _room.Name = name;
        return this;
    }

    public RoomBuilder WithDifficulty(string difficulty)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  ✓ Setting difficulty: {difficulty}");
        Console.ResetColor();
        _room.Difficulty = difficulty;
        return this;
    }

    public RoomBuilder WithTimeLimit(int seconds)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  ✓ Setting time limit: {seconds}s");
        Console.ResetColor();
        _room.TimeLimit = seconds;
        return this;
    }

    public RoomBuilder WithDescription(string description)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  ✓ Setting description");
        Console.ResetColor();
        _room.Description = description;
        return this;
    }

    public RoomBuilder AddPuzzle(Puzzle puzzle)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"  ✓ Adding puzzle: {puzzle.GetPuzzleType()}");
        Console.ResetColor();
        _room.Puzzles.Add(puzzle);
        return this;
    }

    public RoomBuilder AddTrap(string trapName)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  ⚠️  Adding trap: {trapName}");
        Console.ResetColor();
        _room.Traps.Add(trapName);
        return this;
    }

    public Room Build()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n✅ Room construction completed!\n");
        Console.ResetColor();
        return _room;
    }
}
