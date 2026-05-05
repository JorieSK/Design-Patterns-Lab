# Design Patterns Lab

A practical laboratory for studying design patterns in C#. It includes six demonstrative projects that showcase different design patterns:

## Projects

### 1. **Meal Ordering System** 🍔
- **Design Pattern Used**: Builder Pattern
- **Description**: A system for building customized meals in an organized manner. It uses the MealBuilder class to construct meals step-by-step, and the MealDirector class to orchestrate the building process and create pre-defined meals.

### 2. **Payment Processing System** 💳
- **Design Pattern Used**: Strategy Pattern
- **Description**: A payment processing system that supports multiple payment strategies (credit card, cash, Apple Pay, STC Pay). It allows switching payment methods easily without modifying client code.

### 3. **MessageProcessor** 📧
- **Design Pattern Used**: Template Method Pattern
- **Description**: A message processor that uses a fixed sequence of steps (prepare greeting, build content, add signature, send, log operation) with different content implementation for each message type.

### 4. **LogicGate** 🎮
- **Design Patterns Used**:
  - **Singleton Pattern** - To manage the game engine (GameEngine)
  - **Builder Pattern** - To construct complex rooms (RoomBuilder)
  - **Template Method Pattern** - To execute puzzles with a fixed sequence
- **Description**: An advanced puzzle game that combines three design patterns:
  - Singleton ensures a single instance of the game engine in a thread-safe manner
  - Builder provides a fluent interface for constructing complex rooms with puzzles and traps
  - Template Method defines a fixed structure for puzzle execution (display question, get answer, validate, display result)

### 5. **Document Processing System** 📄
- **Design Pattern Used**: Template Method Pattern
- **Description**: A document processing system that uses a unified sequence of steps (open file, validate format, process content, save result, log completion) with different implementations for each document type (PDF, Word, Excel).

### 6. **SalesTracker** 📊
- **Design Pattern Used**: Dependency Injection Pattern
- **Description**: A sales tracking system that uses dependency injection with different service lifetimes: Singleton (shared across the application), Scoped (shared within a single scope), and Transient (new instance each time).
