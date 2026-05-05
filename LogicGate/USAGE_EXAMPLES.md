# Usage Examples & Implementation Guide

## Quick Start

### Running the Game
```bash
cd C:\Users\HP\Desktop\DBT\LogicGate\
dotnet run
```

Follow the prompts and answer puzzles to escape the dungeon!

---

## Pattern Usage Examples

### 1. Using the Singleton Pattern

#### Accessing the Game Engine Anywhere:
```csharp
// In any file, you can access the single GameEngine instance
var engine = GameEngine.Instance;

// Display current status
engine.DisplayGameStatus();

// Update game state
engine.AddScore(10);
engine.DecreaseAttempts();
engine.AdvanceRoom();

// Check game status
if (engine.IsGameActive)
{
	Console.WriteLine($"Score: {engine.Score}");
	Console.WriteLine($"Attempts: {engine.AttemptsRemaining}");
}
```

#### Thread-Safe Property:
```csharp
// The Instance property is thread-safe and creates only one instance
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
```

---

### 2. Using the Builder Pattern

#### Creating Simple Rooms:
```csharp
// Easy room with single puzzle
var easyRoom = new RoomBuilder()
	.WithName("Tutorial Room")
	.WithDifficulty("Easy")
	.WithTimeLimit(120)
	.WithDescription("A simple introduction.")
	.AddPuzzle(new MathPuzzle())
	.Build();
```

#### Creating Complex Rooms:
```csharp
// Hard room with multiple puzzles and traps
var hardRoom = new RoomBuilder()
	.WithName("The Devil's Chamber")
	.WithDifficulty("Impossible")
	.WithTimeLimit(600)
	.WithDescription("Only the strongest minds survive...")
	.AddPuzzle(new MathPuzzle())
	.AddPuzzle(new PasswordPuzzle())
	.AddPuzzle(new LogicPuzzle())
	.AddPuzzle(new CodePuzzle())
	.AddTrap("Poison Gas")
	.AddTrap("Electric Floor")
	.AddTrap("Collapsing Ceiling")
	.AddTrap("Laser Grid")
	.Build();
```

#### Fluent API Benefits:
```csharp
// Instead of many overloaded constructors:
// ❌ BAD:
var room = new Room(
	"Name", 
	"Difficulty", 
	300, 
	"Description", 
	new List<Puzzle>(), 
	new List<string>()
);

// ✅ GOOD (Fluent Builder):
var room = new RoomBuilder()
	.WithName("Name")
	.WithDifficulty("Difficulty")
	.WithTimeLimit(300)
	.WithDescription("Description")
	.Build();
```

---

### 3. Using the Template Method Pattern

#### Executing a Puzzle:
```csharp
// The Template Method ensures consistent flow
var puzzle = new MathPuzzle();

// Execute runs the predefined algorithm:
// 1. Display header
// 2. Show question
// 3. Get input
// 4. Validate
// 5. Display result
bool solved = puzzle.Execute();

if (solved)
{
	Console.WriteLine("Player solved the puzzle!");
}
```

#### Creating New Puzzle Types:
```csharp
// Create a new puzzle by extending Puzzle base class
public class HistoryPuzzle : Puzzle
{
	public HistoryPuzzle() 
		: base(
			"In what year did World War II end?", 
			"1945", 
			30
		)
	{
	}

	public override string GetPuzzleType() => "History";

	// Override ValidateInput for specific logic if needed
	protected override bool ValidateInput(string playerAnswer)
	{
		// Custom validation: accept "1945", "nineteen forty-five", etc.
		string normalized = playerAnswer.ToLower().Replace(" ", "");
		return normalized == "1945" || normalized == "nineteenfortyfive";
	}
}

// Use it immediately:
var room = new RoomBuilder()
	.WithName("History Room")
	.AddPuzzle(new HistoryPuzzle())
	.Build();
```

#### Template Method Algorithm:
```csharp
public bool Execute()
{
	DisplayHeader();                    // Step 1: "PUZZLE: MathPuzzle"
	DisplayQuestion();                  // Step 2: "❓ Solve: 5 + (2 * 3)?"
	string playerAnswer = GetPlayerInput();  // Step 3: Read Console.ReadLine()
	bool isCorrect = ValidateInput(playerAnswer);  // Step 4: Check answer
	DisplayResult(isCorrect);           // Step 5: Show result

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
```

---

## Real-World Scenario: Adding a New Feature

### Scenario: Add a Multiple-Choice Puzzle

#### Step 1: Create the new puzzle class
```csharp
public class MultipleChoicePuzzle : Puzzle
{
	private List<string> _options;
	private int _correctOptionIndex;

	public MultipleChoicePuzzle(
		string question, 
		List<string> options, 
		int correctIndex, 
		int points)
		: base(question, options[correctIndex], points)
	{
		_options = options;
		_correctOptionIndex = correctIndex;
	}

	public override string GetPuzzleType() => "Multiple Choice";

	protected override void DisplayQuestion()
	{
		base.DisplayQuestion();
		for (int i = 0; i < _options.Count; i++)
		{
			Console.WriteLine($"  {i + 1}. {_options[i]}");
		}
	}

	protected override bool ValidateInput(string playerAnswer)
	{
		if (int.TryParse(playerAnswer, out int choice))
		{
			return choice - 1 == _correctOptionIndex;
		}
		return false;
	}
}
```

#### Step 2: Add it to a room (no changes needed to Room or Builder!)
```csharp
var puzzles = new List<string> 
{ 
	"Paris", 
	"London", 
	"Berlin", 
	"Rome" 
};

var room = new RoomBuilder()
	.WithName("Geography Room")
	.WithDifficulty("Medium")
	.AddPuzzle(new MultipleChoicePuzzle(
		"What is the capital of France?",
		puzzles,
		0,  // "Paris" is correct (index 0)
		18
	))
	.Build();
```

#### Step 3: Use it immediately!
```csharp
foreach (var puzzle in room.Puzzles)
{
	puzzle.Execute();  // Works for MultipleChoicePuzzle too!
}
```

**Result:** No changes to existing code! Template Method + Inheritance = Extensibility! ✅

---

## Game Manager Orchestration

### How GameManager Ties Everything Together:

```csharp
public class GameManager
{
	public void Play()
	{
		// Get the Singleton (only one instance ever created)
		var engine = GameEngine.Instance;

		// Build rooms using Builder Pattern
		InitializeGame();  // Creates rooms with RoomBuilder

		// Execute puzzles using Template Method
		while (engine.IsGameActive && _currentRoomIndex < _rooms.Count)
		{
			PlayRoom(_rooms[_currentRoomIndex]);  // Calls puzzle.Execute()
			_currentRoomIndex++;
		}

		DisplayGameEnd();
	}

	private void PlayRoom(Room room)
	{
		room.DisplayRoomInfo();
		engine.DisplayGameStatus();  // Uses Singleton

		// Template Method in action for each puzzle
		foreach (var puzzle in room.Puzzles)
		{
			bool solved = puzzle.Execute();  // Template Method Pattern
			engine.DisplayGameStatus();      // Singleton usage
		}
	}
}
```

---

## Testing Examples

### Unit Testing the Singleton:
```csharp
[Test]
public void GameEngine_InstanceIsAlwaysSame()
{
	var instance1 = GameEngine.Instance;
	var instance2 = GameEngine.Instance;

	Assert.AreSame(instance1, instance2);  // Same object
}

[Test]
public void GameEngine_StateIsPersistent()
{
	var engine = GameEngine.Instance;
	engine.AddScore(10);

	// Get "new" reference (but same instance)
	var engine2 = GameEngine.Instance;

	Assert.AreEqual(10, engine2.Score);  // State persisted!
}
```

### Testing the Builder:
```csharp
[Test]
public void RoomBuilder_CreatesRoomWithCorrectPuzzles()
{
	var room = new RoomBuilder()
		.WithName("Test Room")
		.AddPuzzle(new MathPuzzle())
		.AddPuzzle(new PasswordPuzzle())
		.Build();

	Assert.AreEqual(2, room.Puzzles.Count);
	Assert.AreEqual("Test Room", room.Name);
}
```

### Testing the Template Method:
```csharp
[Test]
public void MathPuzzle_CorrectAnswerReturnsTrue()
{
	var puzzle = new MathPuzzle();
	var result = puzzle.Execute();  // User would input "11"

	Assert.IsTrue(result);
}
```

---

## Performance Considerations

### Singleton Performance:
```csharp
// First call: Thread lock + creation (slight overhead)
GameEngine.Instance;  // ~microseconds

// Subsequent calls: Direct reference (negligible overhead)
GameEngine.Instance;  // ~nanoseconds
GameEngine.Instance;  // ~nanoseconds
```

### Builder Performance:
```csharp
// Method chaining is just function calls (very fast)
var room = new RoomBuilder()
	.WithName("X")          // O(1)
	.AddPuzzle(new Puzzle())  // O(1) List.Add
	.Build();               // O(1) - just returns _room
```

### Template Method Performance:
```csharp
// Virtual method calls have minimal overhead in .NET
puzzle.Execute();  // Single virtual dispatch + method body
```

**Conclusion:** All three patterns have negligible performance impact! ✅

---

## Common Questions

### Q: Why not use static methods instead of Singleton?
**A:** Singleton can be instantiated and inherited; static classes cannot. Singleton is thread-safe for lazy initialization.

### Q: Why not use constructors instead of Builder?
**A:** Builder creates readable, self-documenting code. With many parameters, constructors become:
- Hard to read: `new Room("Lab", "Easy", 300, "...", ...)`
- Error-prone: Wrong parameter order
- Unmaintainable: Existing code breaks when adding parameters

### Q: Can I override Execute() in a puzzle subclass?
**A:** Not recommended - Template Method should be sealed to prevent breaking the algorithm. Override the steps instead!

### Q: How do I add new puzzle types?
**A:** Just inherit from `Puzzle` and override `GetPuzzleType()` and any `Display*` or `Validate*` methods you need. No other changes required!

### Q: Is this scalable?
**A:** Yes! The design scales to hundreds of puzzles and rooms without modification.

---

## Next Steps for Enhancement

### 1. Add Difficulty Selection:
```csharp
Console.WriteLine("Select difficulty: 1) Easy 2) Medium 3) Hard");
int choice = int.Parse(Console.ReadLine());
var difficulty = choice switch 
{
	1 => BuildEasyGame(),
	2 => BuildMediumGame(),
	3 => BuildHardGame(),
	_ => BuildMediumGame()
};
```

### 2. Save High Scores:
```csharp
public class ScoreManager
{
	public void SaveScore(string playerName, int score)
	{
		// Save to file using JSON
	}

	public List<(string Name, int Score)> LoadTopScores(int count)
	{
		// Load from file
	}
}
```

### 3. Add Hint System:
```csharp
public abstract class Puzzle
{
	protected virtual string GetHint() => "No hint available.";

	// Subclasses can override for hints
}
```

### 4. Implement Game Persistence:
```csharp
public class GameSaveManager
{
	public void SaveGame(GameEngine engine, int currentRoom)
	{
		// Serialize state to JSON
	}

	public void LoadGame()
	{
		// Deserialize state from JSON
	}
}
```

---

## Summary

The Escape Room Builder demonstrates:
✅ **Singleton** - One game engine for consistent state  
✅ **Builder** - Flexible, readable room construction  
✅ **Template Method** - Consistent puzzle execution with variations  
✅ **SOLID Principles** - Single responsibility, open/closed, etc.  
✅ **Clean Code** - Self-documenting, maintainable architecture  
✅ **Extensibility** - Easy to add new puzzle types or rooms  
✅ **Professional Quality** - Interview-ready implementation  

Perfect for technical presentations and code reviews! 🚀
