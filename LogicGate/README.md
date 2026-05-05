# 🎮 Escape Room Builder - Design Patterns Showcase

## 📋 Project Overview

A terminal-based interactive escape room game that demonstrates three powerful design patterns working together:
- **Singleton Pattern** (GameEngine)
- **Builder Pattern** (RoomBuilder)
- **Template Method Pattern** (Puzzle classes)

## 🏗️ Architecture & Design Patterns

### 1. **Singleton Pattern** - GameEngine.cs
**Purpose:** Ensures a single instance of the game engine throughout the application.

**Key Features:**
- Thread-safe initialization using double-checked locking
- Manages global game state: Score, Attempts, Time, Room Number
- Provides centralized status display

**Why it matters:**
- Guarantees consistent game state across all rooms and puzzles
- Prevents duplicate game instances that could cause state inconsistency
- Easy access via `GameEngine.Instance` from anywhere in the code

```csharp
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
				}
			}
		}
		return _instance;
	}
}
```

---

### 2. **Builder Pattern** - Room.cs
**Purpose:** Constructs complex Room objects with fluent interface, avoiding constructor overloading.

**Key Features:**
- Fluent chain method calls: `.WithName().WithDifficulty().AddPuzzle().Build()`
- Flexible room configuration without modifying Room class
- Clear, readable room construction process
- Visual feedback during building phase

**Why it matters:**
- Easy to create different room configurations (Easy, Medium, Hard)
- Modular: Add/remove puzzles and traps without changing Room constructor
- Self-documenting code through method names

```csharp
var room = new RoomBuilder()
	.WithName("The Haunted Lab")
	.WithDifficulty("Easy")
	.WithTimeLimit(300)
	.AddPuzzle(new MathPuzzle())
	.AddTrap("Gas Leak")
	.Build();
```

**Timeline in Output:**
1. "Building Room 1 - The Haunted Lab:"
2. ✓ Setting room name: 'The Haunted Lab'
3. ✓ Setting difficulty: Easy
4. ... (other settings)
5. ✓ Adding puzzle: Mathematics
6. ⚠️ Adding trap: Gas Leak
7. ✅ Room construction completed!

---

### 3. **Template Method Pattern** - Puzzle.cs & Puzzles.cs
**Purpose:** Defines the skeleton of puzzle execution; subclasses implement specific logic.

**Key Features:**
- Base class `Puzzle` defines execution algorithm: DisplayHeader → DisplayQuestion → GetInput → ValidateInput → DisplayResult
- Subclasses implement specific puzzle types: MathPuzzle, PasswordPuzzle, LogicPuzzle, CodePuzzle
- Consistent UI/UX flow across all puzzle types
- Easy to extend with new puzzle types

**Why it matters:**
- Enforces consistent puzzle flow regardless of puzzle type
- Reduces code duplication (no repeated input/output logic)
- New puzzle types only need to override relevant methods
- Maintains clear separation of concerns

```csharp
public bool Execute()
{
	DisplayHeader();           // Template step 1
	DisplayQuestion();         // Template step 2
	string playerAnswer = GetPlayerInput();  // Template step 3
	bool isCorrect = ValidateInput(playerAnswer);  // Template step 4
	DisplayResult(isCorrect);  // Template step 5
	return isCorrect;
}
```

**Concrete Implementations:**

| Puzzle Type | Question | Answer | Points | Special Logic |
|------------|----------|--------|--------|--------------|
| MathPuzzle | Solve: 5 + (2 * 3) | 11 | 10 | Numeric validation |
| PasswordPuzzle | Enter password: (Hint: Legendary treasure) | GOLD | 15 | Text comparison |
| LogicPuzzle | Riddle about geography | MAP | 20 | Riddle solving |
| CodePuzzle | Decode binary sequence | Hello | 25 | Binary interpretation |

---

## 🎮 Game Flow

```
┌─────────────────────────────────────┐
│   Program.cs Main()                 │
└─────────────────────────────────────┘
		  ↓
┌─────────────────────────────────────┐
│   Initialize GameEngine (Singleton) │
│   Display System Header             │
└─────────────────────────────────────┘
		  ↓
┌─────────────────────────────────────┐
│   Create GameManager                │
│   Call Play()                       │
└─────────────────────────────────────┘
		  ↓
┌─────────────────────────────────────┐
│   InitializeGame()                  │
│   Build 3 Rooms using RoomBuilder   │
│   (Builder Pattern in action)       │
└─────────────────────────────────────┘
		  ↓
┌─────────────────────────────────────┐
│   For Each Room:                    │
│   ├─ Display Room Info              │
│   ├─ Show GameEngine Status         │
│   └─ Execute Puzzles (Template)     │
└─────────────────────────────────────┘
		  ↓
┌─────────────────────────────────────┐
│   Display Final Results             │
│   Show Total Score                  │
└─────────────────────────────────────┘
```

---

## 🎯 Game Rules

- **Start Attempts:** 3
- **Time Limit:** 300 seconds (5 minutes)
- **Scoring:**
  - Math Puzzle: +10 points
  - Password Puzzle: +15 points
  - Logic Puzzle: +20 points
  - Code Puzzle: +25 points
- **Loss Condition:** Attempts reach 0
- **Win Condition:** Complete all rooms with correct puzzle answers

---

## 📊 Terminal Output Example

```
╔═══════════════════════════════════════════════════╗
║       🎮 ESCAPE ROOM BUILDER - GAME ENGINE 🎮      ║
║              Singleton Instance: #001              ║
╚═══════════════════════════════════════════════════╝

==================================================
🏗️  BUILDING THE DUNGEON USING BUILDER PATTERN 🏗️
==================================================

Building Room 1 - The Haunted Lab:
  ✓ Setting room name: 'The Haunted Lab'
  ✓ Setting difficulty: Easy
  ✓ Setting time limit: 300s
  ✓ Setting description
  ✓ Adding puzzle: Mathematics
  ⚠️  Adding trap: Gas Leak
✅ Room construction completed!

[Room Info Displayed]
[Press any key to continue...]

┌─────────────────────────────────────┐
│  PUZZLE: MathPuzzle                 │
└─────────────────────────────────────┘

❓ Solve: 5 + (2 * 3) = ?
➤ Your answer: 11
✓ Correct! Door Unlocked. +10 Points!

╔════════════════════════════════════════╗
║  Room: 1     | Score: 10   | Attempts: 3   ║
║  Time Remaining: 300s               ║
║  Status: ACTIVE                       ║
╚════════════════════════════════════════╝
```

---

## 🔧 How to Run

```bash
cd C:\Users\HP\Desktop\DBT\LogicGate\
dotnet run
```

Follow the on-screen prompts to:
1. Press any key to proceed between screens
2. Enter answers to puzzles
3. Try to escape the dungeon before running out of attempts!

---

## 📚 Design Pattern Benefits

### Why These 3 Patterns?

1. **Singleton** - Solves the problem of managing game state consistency
2. **Builder** - Solves the problem of complex object construction with many options
3. **Template Method** - Solves the problem of code duplication in similar algorithms

### Educational Value

- **Clear Separation of Concerns:** Each class has a single responsibility
- **DRY Principle:** Code is not repeated (Template Method)
- **Easy to Extend:** Adding new puzzle types requires minimal code
- **Professional Structure:** Industry-standard patterns used in real applications
- **Interview-Ready:** Demonstrates understanding of SOLID principles

---

## 🎨 Code Quality

- ✅ Clean, readable code with XML documentation
- ✅ Proper encapsulation and access modifiers
- ✅ Consistent naming conventions
- ✅ No magic numbers (constants defined)
- ✅ Exception handling ready (can be added)
- ✅ Follows SOLID principles

---

## 🚀 Future Enhancements

- Add timer countdown (actual elapsed time tracking)
- Add hint system
- Persistent high scores to file
- More puzzle types and rooms
- Difficulty level selection at start
- Leaderboard system
- Save/Load game progress

---

## 📝 Project Structure

```
LogicGate/
├── Program.cs           (Entry point)
├── GameEngine.cs        (Singleton pattern)
├── GameManager.cs       (Game orchestration)
├── Room.cs             (Room + Builder pattern)
├── Puzzle.cs           (Template method base class)
├── Puzzles.cs          (Concrete puzzle implementations)
└── README.md           (This file)
```

---

## 👨‍💻 Author Notes

This project demonstrates how design patterns make code more maintainable and extensible. By combining these three patterns, we achieve:

- **Single Responsibility:** Each class does one thing well
- **Open/Closed Principle:** Open for extension (new puzzles), closed for modification
- **Liskov Substitution:** All puzzle types work interchangeably
- **Dependency Inversion:** High-level modules don't depend on low-level details

Perfect for a technical presentation or interview!
