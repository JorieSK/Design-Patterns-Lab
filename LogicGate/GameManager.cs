using System;
using System.Collections.Generic;

/// GameManager: Orchestrates the game flow
/// Uses the Singleton (GameEngine) and Builder (RoomBuilder) patterns
public class GameManager
{
    private List<Room> _rooms;
    private int _currentRoomIndex;
    private GameEngine _engine;

    public GameManager()
    {
        _engine = GameEngine.Instance;
        _rooms = new List<Room>();
        _currentRoomIndex = 0;
    }

    /// Initializes the game by building rooms using the Builder pattern
    public void InitializeGame()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n" + new string('═', 50));
        Console.WriteLine("🏗️  BUILDING THE DUNGEON USING BUILDER PATTERN 🏗️");
        Console.WriteLine(new string('═', 50) + "\n");
        Console.ResetColor();

        // Room 1: The Laboratory
        Console.WriteLine("Building Room 1 - The Haunted Lab:");
        var room1 = new RoomBuilder()
            .WithName("The Haunted Lab")
            .WithDifficulty("Easy")
            .WithTimeLimit(300)
            .WithDescription("A secret lab with strange machines and hidden signs.")
            .AddPuzzle(new MathPuzzle())
            .AddTrap("Gas Leak")
            .Build();
        _rooms.Add(room1);

        System.Threading.Thread.Sleep(500);

        // Room 2: The Cipher Chamber
        Console.WriteLine("\nBuilding Room 2 - The Cipher Chamber:");
        var room2 = new RoomBuilder()
            .WithName("The Cipher Chamber")
            .WithDifficulty("Medium")
            .WithTimeLimit(400)
            .WithDescription("Old walls full of hidden codes and secret messages")
            //.AddPuzzle(new PasswordPuzzle())
            .AddPuzzle(new CodePuzzle())
            .AddTrap("Spikes")
            .AddTrap("Laser Grid")
            .Build();
        _rooms.Add(room2);

        System.Threading.Thread.Sleep(500);

        // Room 3: The Riddle Hall
        Console.WriteLine("\nBuilding Room 3 - The Riddle Hall:");
        var room3 = new RoomBuilder()
            .WithName("The Riddle Hall")
            .WithDifficulty("Hard")
            .WithTimeLimit(500)
            .WithDescription("A big hall full of old riddles. The last test starts now.")
            .AddPuzzle(new LogicPuzzle())
            //.AddPuzzle(new MathPuzzle())
            .AddPuzzle(new PasswordPuzzle())
            .AddTrap("Collapsing Floor")
            .AddTrap("False Exits")
            .Build();
        _rooms.Add(room3);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n" + new string('═', 50));
        Console.WriteLine("✅ DUNGEON FULLY CONSTRUCTED - ALL ROOMS READY ✅");
        Console.WriteLine(new string('═', 50) + "\n");
        Console.ResetColor();

        PauseGame();
    }

    /// <summary>
    /// Runs the game loop
    /// </summary>
    public void Play()
    {
        InitializeGame();

        while (_engine.IsGameActive && _currentRoomIndex < _rooms.Count)
        {
            PlayRoom(_rooms[_currentRoomIndex]);

            if (_engine.AttemptsRemaining <= 0)
            {
                break;
            }

            if (_currentRoomIndex < _rooms.Count - 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n🚪 Moving to the next room...\n");
                Console.ResetColor();
                _currentRoomIndex++;
                _engine.AdvanceRoom();
                PauseGame();
            }
            else
            {
                break;
            }
        }

        DisplayGameEnd();
    }

    /// <summary>
    /// Plays a single room
    /// </summary>
    private void PlayRoom(Room room)
    {
        room.DisplayRoomInfo();
        _engine.DisplayGameStatus();
        PauseGame();

        int correctPuzzles = 0;
        foreach (var puzzle in room.Puzzles)
        {
            if (!_engine.IsGameActive)
            {
                break;
            }

            bool solved = puzzle.Execute();
            if (solved)
            {
                correctPuzzles++;
            }

            _engine.DisplayGameStatus();

            if (_engine.AttemptsRemaining > 0)
            {
                PauseGame();
            }
        }

        if (correctPuzzles == room.Puzzles.Count)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n🎉 YOU ESCAPED {room.Name}! 🎉\n");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Displays the game end screen
    /// </summary>
    private void DisplayGameEnd()
    {
        Console.Clear();

        if (_engine.AttemptsRemaining <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║          GAME OVER - YOU LOST!         ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine($"\nFinal Score: {_engine.Score}");
            Console.ResetColor();
        }
        else if (_currentRoomIndex >= _rooms.Count - 1)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║      🎊 YOU ESCAPED THE DUNGEON! 🎊     ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine($"\n✨ Final Score: {_engine.Score}");
            Console.WriteLine($"✨ Rooms Completed: {_engine.RoomNumber}/{_rooms.Count}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nPress any key to exit...");
        Console.ResetColor();
        Console.ReadKey();
    }

    private void PauseGame()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("[Press any key to continue...]");
        Console.ResetColor();
        Console.ReadKey();
    }
}
