# Quick Reference Guide

## 📁 Project Files Overview

| File | Purpose | Pattern |
|------|---------|---------|
| `Program.cs` | Entry point | N/A |
| `GameEngine.cs` | Manages game state | **Singleton** |
| `GameManager.cs` | Orchestrates game flow | N/A |
| `Room.cs` | Room & builder | **Builder** |
| `Puzzle.cs` | Base puzzle class | **Template Method** |
| `Puzzles.cs` | Concrete puzzles | Template Method (impl) |

---

## 🎯 The 3 Patterns at a Glance

### SINGLETON - GameEngine.cs
```
❌ Problem: Multiple game instances causing state conflicts
✅ Solution: Only ONE instance throughout entire application

Key Code:
private static GameEngine _instance;
public static GameEngine Instance { get; }

Usage:
var engine = GameEngine.Instance;
engine.AddScore(10);
```

**When GameEngine is created:**
```
Program.cs → GameEngine.Instance → Called ONCE
   ↓
Thread-safe creation:
  - Check if null (no lock)
  - If null, acquire lock
  - Check if null again
  - Create instance
  ↓
All subsequent calls return same instance
```

---

### BUILDER - RoomBuilder & Room.cs
```
❌ Problem: Complex Room construction with many parameters
✅ Solution: Fluent interface with method chaining

Key Code:
new RoomBuilder()
	.WithName("Lab")
	.WithDifficulty("Easy")
	.AddPuzzle(new MathPuzzle())
	.Build()

Benefits:
- Readable: Clear what each method does
- Flexible: Add/remove options without changing Room class
- Chainable: Each method returns 'this'
```

**Build Process Flow:**
```
new RoomBuilder()
	↓
Reset() [Initialize empty room]
	↓
WithName() / WithDifficulty() / WithTimeLimit() / WithDescription()
	↓
AddPuzzle() [Can call multiple times]
	↓
AddTrap() [Can call multiple times]
	↓
Build() [Returns configured Room]
```

---

### TEMPLATE METHOD - Puzzle.cs & Puzzles.cs
```
❌ Problem: Duplicate code across different puzzle types
✅ Solution: Define algorithm skeleton, let subclasses fill in details

Key Code:
public bool Execute()  // Template - defines the algorithm
{
	DisplayHeader();           // Step 1
	DisplayQuestion();         // Step 2
	string answer = GetPlayerInput();  // Step 3
	bool correct = ValidateInput(answer);  // Step 4
	DisplayResult(correct);    // Step 5
	return correct;
}

Subclasses override specific steps:
- MathPuzzle: Overrides ValidateInput() to parse as int
- PasswordPuzzle: Uses default string comparison
- LogicPuzzle: Uses default string comparison
- CodePuzzle: Uses default string comparison
```

**Execution Flow:**
```
puzzle.Execute() [Template Method]
	↓
DisplayHeader() [Fixed - always runs]
	↓
DisplayQuestion() [Fixed - always runs]
	↓
GetPlayerInput() [Fixed - always runs]
	↓
ValidateInput() [FLEXIBLE - override if needed]
	↓
DisplayResult() [Fixed - always runs]
	↓
Return bool
```

---

## 🔄 Game Execution Flow

```
START (Program.cs)
  │
  ├─ Initialize: GameEngine.Instance
  │   ├─ Singleton lock acquired
  │   ├─ Create ONE GameEngine
  │   ├─ Display header
  │   └─ Lock released
  │
  ├─ Create: new GameManager()
  │
  └─ Call: gameManager.Play()
	 │
	 ├─ Call: InitializeGame()
	 │   ├─ Console.WriteLine("Building...")
	 │   │
	 │   ├─ Room 1: new RoomBuilder() (Builder Pattern)
	 │   │   ├─ WithName("The Haunted Lab")
	 │   │   ├─ AddPuzzle(new MathPuzzle())
	 │   │   └─ Build()
	 │   │
	 │   ├─ Room 2: new RoomBuilder() (Builder Pattern)
	 │   │   ├─ WithName("The Cipher Chamber")
	 │   │   ├─ AddPuzzle(new PasswordPuzzle())
	 │   │   ├─ AddPuzzle(new CodePuzzle())
	 │   │   └─ Build()
	 │   │
	 │   └─ Room 3: new RoomBuilder() (Builder Pattern)
	 │       ├─ WithName("The Riddle Hall")
	 │       ├─ AddPuzzle(new LogicPuzzle())
	 │       └─ Build()
	 │
	 ├─ WHILE game active AND rooms remaining:
	 │   │
	 │   ├─ Call: PlayRoom(room1)
	 │   │   ├─ room1.DisplayRoomInfo()
	 │   │   ├─ GameEngine.Instance.DisplayGameStatus() [Singleton]
	 │   │   │
	 │   │   └─ FOR each puzzle in room1.Puzzles:
	 │   │       │
	 │   │       ├─ Call: puzzle.Execute() [Template Method]
	 │   │       │   ├─ DisplayHeader()
	 │   │       │   ├─ DisplayQuestion()
	 │   │       │   ├─ GetPlayerInput()
	 │   │       │   ├─ ValidateInput() [Could be overridden]
	 │   │       │   ├─ DisplayResult()
	 │   │       │   └─ Update GameEngine [Singleton]
	 │   │       │
	 │   │       └─ GameEngine.Instance.DisplayGameStatus() [Singleton]
	 │   │
	 │   └─ ADVANCE to next room
	 │
	 └─ Call: DisplayGameEnd()

END
```

---

## 🎮 Game Rules Quick Sheet

| Rule | Value |
|------|-------|
| Starting Attempts | 3 |
| Attempts Lost Per Wrong Answer | 1 |
| Attempts Lost If All Go Wrong | Game Over |
| Time Limit Per Game | 300 seconds (5 min) |
| **MathPuzzle Points** | **10** |
| **PasswordPuzzle Points** | **15** |
| **LogicPuzzle Points** | **20** |
| **CodePuzzle Points** | **25** |
| Win Condition | Complete all 3 rooms |
| Lose Condition | Attempts reach 0 |

---

## 💻 Code Snippets - Quick Copy-Paste

### Create a Room
```csharp
var room = new RoomBuilder()
	.WithName("My Room")
	.WithDifficulty("Medium")
	.WithTimeLimit(300)
	.WithDescription("Description here")
	.AddPuzzle(new MathPuzzle())
	.AddTrap("Trap Name")
	.Build();
```

### Access Game State
```csharp
var engine = GameEngine.Instance;
Console.WriteLine($"Score: {engine.Score}");
Console.WriteLine($"Attempts: {engine.AttemptsRemaining}");
Console.WriteLine($"Room: {engine.RoomNumber}");
```

### Create a Custom Puzzle
```csharp
public class CustomPuzzle : Puzzle
{
	public CustomPuzzle() 
		: base("Question here?", "answer", 25)
	{
	}

	public override string GetPuzzleType() => "Custom";

	protected override bool ValidateInput(string playerAnswer)
	{
		// Custom validation logic
		return playerAnswer.Equals("answer", StringComparison.OrdinalIgnoreCase);
	}
}
```

### Execute a Puzzle
```csharp
var puzzle = new MathPuzzle();
bool solved = puzzle.Execute();
if (solved)
	Console.WriteLine("Correct!");
else
	Console.WriteLine("Wrong!");
```

---

## 🔍 Finding Code Locations

| Need to find... | Location |
|-----------------|----------|
| Game state (Score, Attempts) | `GameEngine.cs` class properties |
| Game initialization | `GameManager.InitializeGame()` method |
| Puzzle execution algorithm | `Puzzle.Execute()` method |
| Room building logic | `RoomBuilder` class |
| Entry point | `Program.Main()` method |
| Console colors/formatting | Throughout (search `Console.ForegroundColor`) |
| Puzzle definitions | `Puzzles.cs` file |

---

## 📊 Class Hierarchy

```
Puzzle (Abstract Base Class)
├── MathPuzzle (overrides ValidateInput)
├── PasswordPuzzle (uses base implementation)
├── LogicPuzzle (uses base implementation)
└── CodePuzzle (uses base implementation)
```

---

## 🧩 How Patterns Work Together

```
GameManager (Orchestrator)
	│
	├─ Uses: GameEngine.Instance [SINGLETON]
	│   └─ Ensures one game state
	│
	├─ Uses: RoomBuilder [BUILDER]
	│   └─ Constructs rooms flexibly
	│
	└─ Uses: puzzle.Execute() [TEMPLATE METHOD]
		└─ Executes puzzles consistently
```

**Result:** Clean separation, easy to extend, professional architecture!

---

## ✨ Key Features

| Feature | Pattern | Location |
|---------|---------|----------|
| Single Game Instance | Singleton | GameEngine.cs |
| Flexible Room Setup | Builder | Room.cs |
| Consistent Puzzle Flow | Template Method | Puzzle.cs |
| Colorful Terminal UI | - | All files |
| ASCII Art Decorations | - | All files |
| Game State Management | Singleton | GameEngine.cs |
| Multiple Puzzle Types | Template Method | Puzzles.cs |
| 3 Progressive Rooms | Builder | GameManager.cs |

---

## 🚀 To Run the Game

```powershell
cd "C:\Users\HP\Desktop\DBT\LogicGate\"
dotnet run
```

**Controls:**
- Press any key to continue
- Type your answer to puzzle questions
- Try to complete all 3 rooms before running out of attempts!

---

## 📈 Complexity Analysis

| Operation | Time | Space |
|-----------|------|-------|
| Access GameEngine.Instance | O(1) | O(1) |
| Create Room with Builder | O(n) | O(n) |
| Execute Puzzle | O(1)* | O(1) |
| Build all rooms | O(n) | O(n) |

*Excludes Console I/O

---

## 🎓 Learning Outcomes

After studying this code, you understand:
- ✅ How Singleton ensures single instance
- ✅ How Builder creates complex objects readably
- ✅ How Template Method reduces code duplication
- ✅ How to combine patterns effectively
- ✅ SOLID principles in practice
- ✅ Thread-safe initialization
- ✅ Inheritance and polymorphism
- ✅ Professional code structure

---

## 🐛 Common Issues & Solutions

| Issue | Solution |
|-------|----------|
| "GameEngine not found" | Add `using` statements or check file location |
| Wrong answer still gives points | Check ValidateInput() override in puzzle |
| Game exits immediately | May need to trace execution flow |
| Unicode characters not showing | Ensure `Console.OutputEncoding = UTF8` in Program.cs |

---

## 📝 Notes for Presentation

**How to explain each pattern:**

1. **Singleton:** "Only one game engine exists to ensure score doesn't reset between rooms"
2. **Builder:** "Building different room configurations without modifying the Room class"
3. **Template Method:** "All puzzles follow the same flow but validate input differently"

**Strong points to mention:**
- Clean, readable code
- Easy to extend (new puzzles need minimal code)
- Professional architecture
- No code duplication
- Follows SOLID principles

---

This quick reference covers everything you need to understand, modify, or present the Escape Room Builder project! 🎉
