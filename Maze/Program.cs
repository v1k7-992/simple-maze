using Maze.Event;
using Maze.Graphics;
using Maze.Spawn;
using Microsoft.Extensions.Configuration;

namespace Maze;

class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddIniFile("settings.ini")
            .Build();
        var settings = new Settings();
        configuration.Bind(settings);

        if (settings.MapHeight <= 0)
            throw new ArgumentOutOfRangeException(nameof(settings.MapHeight),
                "Map height cannot be zero!");
        else if (settings.MapWidth <= 0)
            throw new ArgumentOutOfRangeException(nameof(settings.MapWidth),
                "Map width cannot be zero!");
        else if (settings.NumberOfLives <= 0)
            throw new ArgumentOutOfRangeException(nameof(settings.MapWidth),
                "Number of lives cannot be zero!");
        
        var renderer = new ConsoleMapRenderer();
        var spawner = new DefaultSpawner();
        var playerExitSpawn = new DefaultPlayerExitSpawner(new Random());
        var game = new Game(settings, renderer, spawner, playerExitSpawn);

        ConsoleKeyInfo keyInfo = default;
        InputResult inputResult = default;
        do
        {
            game.GameLoop();
            keyInfo = Console.ReadKey();
            Event.PlayerMovementEventArgs eventArgs = new PlayerMovementEventArgs(); 
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    eventArgs = new PlayerMovementEventArgs(Direction.Up);
                    break;
                case ConsoleKey.DownArrow:
                    eventArgs = new PlayerMovementEventArgs(Direction.Down);
                    break;
                case ConsoleKey.RightArrow:
                    eventArgs = new PlayerMovementEventArgs(Direction.Right);
                    break;
                case ConsoleKey.LeftArrow:
                    eventArgs = new PlayerMovementEventArgs(Direction.Left);
                    break;
            }
            
            // Accept user input and check what result the game says will happen from accepted input.
            inputResult = game.AcceptInput(eventArgs);
            
            if(inputResult.GameState == GameState.PlayerIsAtExit)
                System.Console.WriteLine("You win!");
            else if(inputResult.GameState == GameState.PlayerDead)
                System.Console.WriteLine("You lose.");
        } 
        while (!InputHelper.IsUserRequestingGameExit(keyInfo) && inputResult.GameState == GameState.PlayerAlive);
    }
}