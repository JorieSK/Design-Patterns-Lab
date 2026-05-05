# Class Diagram - Escape Room Builder

## Visual Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                     ESCAPE ROOM BUILDER                         │
│                    Design Patterns Demo                         │
└─────────────────────────────────────────────────────────────────┘

╔═══════════════════════════════════════════════════════════════════╗
║                    SINGLETON PATTERN                             ║
╠═══════════════════════════════════════════════════════════════════╣
║                         GameEngine                               ║
╠═══════════════════════════════════════════════════════════════════╣
║ - _instance: GameEngine (static)                                ║
║ - _lockObject: object (static)                                  ║
│ - Score: int                                                    ║
║ - AttemptsRemaining: int                                        ║
║ - TimeRemaining: int                                            ║
║ - IsGameActive: bool                                            ║
║ - RoomNumber: int                                               ║
╠═══════════════════════════════════════════════════════════════════╣
║ + Instance: GameEngine (property)  ◄──┐ Thread-safe access     ║
║ + DisplayGameStatus(): void              │ Only ONE instance    ║
║ + AddScore(points): void                │ throughout app       ║
║ + DecreaseAttempts(): void              │                      ║
║ + AdvanceRoom(): void                   │                      ║
║ + ResetGameState(): void             ◄──┘                      ║
╚═══════════════════════════════════════════════════════════════════╝
							▲
							│ uses
							│
┌────────────────────────────────────────────────────────────────────┐
║                       GAME MANAGER                                ║
╠════════════════════════════════════════════════════════════════════╣
║ - _rooms: List<Room>                                              ║
║ - _currentRoomIndex: int                                          ║
║ - _engine: GameEngine                                             ║
╠════════════════════════════════════════════════════════════════════╣
║ + InitializeGame(): void     ◄── Uses Builder Pattern             ║
║ + Play(): void               ◄── Orchestrates game flow           ║
║ + PlayRoom(room): void       ◄── Executes Template Methods        ║
║ + DisplayGameEnd(): void                                          ║
╚════════════════════════════════════════════════════════════════════╝
						▲ contains
						│
						├─ creates using
						└─────────┐
								  │
					┌─────────────────────────────────────┐
					│    BUILDER PATTERN                  │
					├─────────────────────────────────────┤
					│          RoomBuilder                │
					├─────────────────────────────────────┤
					│ - _room: Room                       │
					├─────────────────────────────────────┤
					│ + Reset(): RoomBuilder              │
					│ + WithName(name): RoomBuilder       │
					│ + WithDifficulty(diff): RoomBuilder │
					│ + WithTimeLimit(sec): RoomBuilder   │
					│ + WithDescription(desc): RoomBuilder│
					│ + AddPuzzle(puzzle): RoomBuilder    │
					│ + AddTrap(trap): RoomBuilder        │
					│ + Build(): Room  ◄── Fluent API    │
					└─────────────────────────────────────┘
						builds        ▼
					┌────────────────────────────────────┐
					│          Room                      │
					├────────────────────────────────────┤
					│ + Name: string                     │
					│ + Difficulty: string               │
					│ + TimeLimit: int                   │
					│ + Description: string              │
					│ + Puzzles: List<Puzzle>            │
					│ + Traps: List<string>              │
					├────────────────────────────────────┤
					│ + DisplayRoomInfo(): void          │
					└────────────────────────────────────┘
							 ▲ contains
							 │
		┌────────────────────┴────────────────────┐
		│                                         │
╔═══════════════════════════════════════════╗   │
║   TEMPLATE METHOD PATTERN                ║   │
╠═══════════════════════════════════════════╣   │
║          Puzzle (Abstract)                ║   │
╠═══════════════════════════════════════════╣   │
║ # _question: string                      ║   │
║ # _correctAnswer: string                 ║   │
║ # _pointsReward: int                     ║   │
╠═══════════════════════════════════════════╣   │
║ + Execute(): bool  ◄─ Template Method    ║   │
║ # DisplayHeader(): void  ◄──┐            ║   │
║ # DisplayQuestion(): void    │ Steps     ║   │
║ # GetPlayerInput(): string   │ of the    ║   │
║ # ValidateInput(ans): bool   │ Template  ║   │
║ # DisplayResult(correct): void◄──┘       ║   │
║ + GetPuzzleType(): string                ║   │
║   (abstract)                             ║   │
╚═══════════════════════════════════════════╝   │
		▲ inherited by                         │
		│                                      │
	┌───┴────────┬──────────────┬──────────────┐
	│            │              │              │
	│            │              │              │
┌───────────┐ ┌──────────────┐ ┌──────────┐ ┌────────────┐
│MathPuzzle │ │PasswordPuzzle│ │LogicPuzzle│ │CodePuzzle  │
├───────────┤ ├──────────────┤ ├──────────┤ ├────────────┤
│ Question: │ │ Question:   │ │ Question:│ │ Question: │
│"5+(2*3)?" │ │"Password?" │ │"MAP?"   │ │"Binary?"  │
│ Answer: 11│ │ Answer:GOLD│ │Answer:MAP│ │Answer:Hello│
│ Points: 10│ │ Points: 15 │ │ Pts: 20 │ │ Points: 25 │
├───────────┤ ├──────────────┤ ├──────────┤ ├────────────┤
│Override:  │ │No override  │ │No override│ │No override │
│ValidateIn │ │needed       │ │needed     │ │needed      │
│Put() for  │ │             │ │           │ │            │
│numeric    │ │             │ │           │ │            │
│validation │ │             │ │           │ │            │
└───────────┘ └──────────────┘ └──────────┘ └────────────┘

```

---

## Component Interactions

### 1. Program.cs Entry Point
```
START
  │
  └─► Initialize: GameEngine.Instance (Singleton Created)
	   │
	   └─► Display System Header
			│
			└─► Create: GameManager
				 │
				 └─► Call: gameManager.Play()
					  │
					  └─► InitializeGame()
						   │ (Builder Pattern Starts)
						   │
						   ├─► new RoomBuilder()
						   ├─► .WithName("Room1")
						   ├─► .WithDifficulty("Easy")
						   ├─► .AddPuzzle(new MathPuzzle())
						   ├─► .AddTrap("Gas Leak")
						   └─► .Build() ──► Room1
								│
								├─► new RoomBuilder()
								├─► .WithName("Room2")
								├─► ... (similar)
								└─► .Build() ──► Room2
									 │
									 ├─► ... (Room3)
									 │
									 └─► PlayRoom()
										  │
										  ├─► For each Puzzle
										  │    │
										  │    ├─► puzzle.Execute()
										  │    │    (Template Method)
										  │    │
										  │    ├─► DisplayHeader()
										  │    ├─► DisplayQuestion()
										  │    ├─► GetPlayerInput()
										  │    ├─► ValidateInput()
										  │    ├─► DisplayResult()
										  │    │
										  │    └─► Update GameEngine
										  │         (Singleton)
										  │
										  └─► DisplayGameStatus()
											   (Via Singleton)
											   │
											   └─► END / NEXT ROOM
```

---

## Method Call Flow for a Single Puzzle

```
Puzzle.Execute() [Template Method]
│
├─ Step 1: DisplayHeader()
│          ├─ Console.ForegroundColor = Magenta
│          ├─ Print: "PUZZLE: MathPuzzle"
│          └─ Console.ResetColor()
│
├─ Step 2: DisplayQuestion()
│          ├─ Console.ForegroundColor = Green
│          ├─ Print: "❓ Solve: 5 + (2 * 3) = ?"
│          └─ Console.ResetColor()
│
├─ Step 3: GetPlayerInput()
│          ├─ Console.ForegroundColor = Cyan
│          ├─ Print: "➤ Your answer: "
│          ├─ Read: Console.ReadLine()
│          └─ Return: player input string
│
├─ Step 4: ValidateInput(playerAnswer)
│          ├─ For MathPuzzle: Parse as int, compare to 11
│          ├─ For PasswordPuzzle: Case-insensitive string compare
│          └─ Return: bool (true/false)
│
├─ Step 5: DisplayResult(isCorrect)
│          ├─ If correct:
│          │  ├─ ForegroundColor = Green
│          │  ├─ Print: "✓ Correct! Door Unlocked. +10 Points!"
│          │  └─ GameEngine.Instance.AddScore(10)
│          │
│          └─ If incorrect:
│             ├─ ForegroundColor = Red
│             ├─ Print: "✗ Wrong Answer!"
│             └─ GameEngine.Instance.DecreaseAttempts()
│
└─ Return: bool (puzzle solved or not)
```

---

## Singleton Thread-Safe Implementation

```
GameEngine.Instance (First Call)
│
├─ _instance == null? ─ YES
│  │
│  └─ lock (_lockObject)
│     │
│     └─ _instance == null? ─ YES
│        │
│        └─ new GameEngine()
│           ├─ Set private fields
│           ├─ Call DisplayHeader()
│           └─ Return instance
│
└─ Return: _instance

GameEngine.Instance (Subsequent Calls)
│
└─ _instance == null? ─ NO (cached)
   │
   └─ Return: _instance (no lock needed)
```

---

## Builder Fluent API Chain

```
new RoomBuilder()
	.Reset()                                    ─┐
	.WithName("The Haunted Lab")                 │
	.WithDifficulty("Easy")                      │ Each method
	.WithTimeLimit(300)                          │ returns 'this'
	.WithDescription("A mysterious...")          │ (RoomBuilder)
	.AddPuzzle(new MathPuzzle())                 │ enabling
	.AddPuzzle(new CodePuzzle())                 │ method
	.AddTrap("Gas Leak")                         │ chaining
	.AddTrap("Spikes")                           │
	.Build()                                    ─┘
	│
	└─► Returns fully configured Room object
		with all puzzles and traps
```

---

## Template Method Extension Points

```
Abstract Puzzle Class
│
├─ execute() [FINAL - Template Algorithm]
│  ├─ displayHeader()
│  ├─ displayQuestion()
│  ├─ getPlayerInput()
│  ├─ validateInput()       ◄─ Virtual (can override)
│  └─ displayResult()
│
├─ Concrete: MathPuzzle
│  └─ Override: validateInput() ─► Parse as int
│
├─ Concrete: PasswordPuzzle
│  └─ No override (default string comparison)
│
├─ Concrete: LogicPuzzle
│  └─ No override (default string comparison)
│
└─ Concrete: CodePuzzle
   └─ No override (default string comparison)

Result: Same execution flow, different validation logic!
```

---

## State Management Through Singleton

```
GameEngine Singleton State
│
├─ Score: 0 ─────────────┬─ AddScore(10) ─► 10
│                         └─ AddScore(15) ─► 25
│
├─ Attempts: 3 ───────────┬─ DecreaseAttempts() ─► 2
│                         └─ DecreaseAttempts() ─► 1
│
├─ RoomNumber: 1 ─────────┬─ AdvanceRoom() ─► 2
│                         └─ AdvanceRoom() ─► 3
│
├─ IsGameActive: true ─────┬─ Attempts ≤ 0 ─► false
│                          └─ Game Over ─► false
│
└─ TimeRemaining: 300
   (Persistent across all rooms)

Accessible from anywhere:
├─ Puzzle.cs ─────► GameEngine.Instance.AddScore()
├─ GameManager.cs ─► GameEngine.Instance.DisplayGameStatus()
└─ Program.cs ────► GameEngine.Instance.ResetGameState()
```

---

This architecture ensures:
✅ Single instance for consistent state (Singleton)
✅ Flexible room creation (Builder)
✅ Reusable puzzle algorithm (Template Method)
✅ Easy to extend with new puzzle types
✅ Clean separation of concerns
✅ Professional, maintainable codebase
