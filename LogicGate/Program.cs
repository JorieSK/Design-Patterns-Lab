using System;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Initialize Singleton GameEngine
        var gameEngine = GameEngine.Instance;

        // Create and start the game
        var gameManager = new GameManager();
        gameManager.Play();
    }
}
