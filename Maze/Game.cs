using Maze.Event;
using Maze.Graphics;
using Maze.Spawn;

namespace Maze;

public class Game
{
    /// <summary>
    /// Represents the map player will traverse
    /// </summary>
    private Map Map;

    /// <summary>
    /// Represents the number of lives the player will have.
    /// </summary>
    private int _Lives;

    /// <summary>
    /// Represents the number of steps the player did, and that will be his score.
    /// </summary>
    private int _Score;

    /// <summary>
    /// Game settings
    /// </summary>
    private Settings _settings;
    
    /// <summary>
    /// Gets the game state
    /// </summary>
    public GameState GameState { get; private set; }
    
    /// <summary>
    /// Random number generator for game functions such as player and exit spawning.
    /// </summary>
    private Random _rng;
    
    /// <summary>
    /// This is the map renderer. It creates a graphical representation of the Map.
    /// </summary>
    private readonly IMapRenderer _mapRenderer;
    
    public Game(Settings settings, IMapRenderer mapRenderer, ISpawner spawner, IPlayerExitSpawner playerExitSpawner)
    {
        _rng = new Random();
        GameState = GameState.PlayerAlive;
        _settings = settings;
        
        Map = new Map(settings, spawner, playerExitSpawner, _rng);
        
        _Lives = settings.NumberOfLives;
        _mapRenderer = mapRenderer;
    }

    public void GameLoop()
    {
        _mapRenderer.Render(Map, _Lives, _Score);
    }

    public InputResult AcceptInput(Event.PlayerMovementEventArgs movementEventArgs)
    {
        // If no input provided or player is ded then don't do anything.
        if (movementEventArgs.Direction == Direction.None || GameState != GameState.PlayerAlive)
            return new InputResult(GameState);

        // Keep record of previous coords
        var newCoords = Map.CurrentPlayerLocation.Copy();
        
        // Check if movement is allowed inside bounds also use vectors
        if (movementEventArgs.Direction == Direction.Up && (newCoords.Y - 1) >= 0)
            newCoords.Y -= 1;
        else if (movementEventArgs.Direction == Direction.Down && (newCoords.Y + 1) <= (Map.Height - 1))
            newCoords.Y += 1;
        else if (movementEventArgs.Direction == Direction.Left && (newCoords.X - 1) >= 0)
            newCoords.X -= 1;
        else if (movementEventArgs.Direction == Direction.Right && newCoords.X + 1 <= Map.Width - 1)
            newCoords.X += 1;

        var movementResult = Map.SetPlayer(newCoords);
            
        if (movementResult.DidPlayerMove)
        {
            if (Map.ExitLocation == Map.CurrentPlayerLocation)
                GameState = GameState.PlayerIsAtExit;
            
            if (movementResult.DidPlayerHitMine)
            {
                _Lives -= 1;
            }

            _Score += 1;
        }

        // Now conclude the game state
        if (CheckIfPlayerDead())
        {
            GameState = GameState.PlayerDead;
        }
        
        return new InputResult(GameState);
    }

    public bool CheckIfPlayerDead()
    {
        return _Lives == 0;
    }
}
