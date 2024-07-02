using System.Text;
using Maze.Event;
using Maze.Graphics;
using Maze.Spawn;
using Xunit.Abstractions;

namespace Maze.UnitTests;

public class RandomizedGameTests
{
    private readonly Map _gameMap;

    private readonly Settings _settings;

    private readonly Random _rng;

    private readonly ISpawner _spawner;

    private readonly IPlayerExitSpawner _playerExitSpawner;

    private readonly Game _game;

    private readonly DebugRenderer _mapRenderer;

    private readonly ITestOutputHelper _moveLogger;

    private readonly List<Direction> _directionsPlayerWent;

    public RandomizedGameTests(ITestOutputHelper moveLogger)
    {
        _settings = new Settings()
        {
            MapHeight = 10,
            MapWidth = 10,
            NumberOfLives = 10
        };

        _rng = new Random();
        _spawner = new DefaultSpawner();
        _playerExitSpawner = new DefaultPlayerExitSpawner(_rng);
        _mapRenderer = new DebugRenderer();
        _game = new Game(_settings, _mapRenderer, _spawner, _playerExitSpawner);
        _moveLogger = moveLogger;
        _directionsPlayerWent = new List<Direction>();
    }
    
    [Theory]
    [InlineData(100, 30)]
    public void Do_Randomized_Tests(int numOfIterations, int numOfPlayerSteps)
    {
        try
        {
            for (var idx = 0; idx < numOfIterations; idx++)
            {
                for (var jdx = 0; jdx < numOfPlayerSteps; jdx++)
                {
                    var direction = RandomDirection();
                    var randomMovment = new PlayerMovementEventArgs(direction);
                    _directionsPlayerWent.Add(direction);
                    var inputResult = _game.AcceptInput(randomMovment);
                    _game.GameLoop();
                    if (inputResult.GameState == GameState.PlayerDead)
                        _moveLogger.WriteLine("Player lost.");
                    else if (inputResult.GameState == GameState.PlayerIsAtExit)
                        _moveLogger.WriteLine("Player won.");
                }
            }
        }
        catch (Exception ex)
        {
            _moveLogger.WriteLine("Player went these directions: {0}", _directionsPlayerWent.Aggregate<Direction, string>("",
                (s, direction) => s + direction.ToString() + ","));
            
            _moveLogger.WriteLine($"Current Map: \n{_mapRenderer.CurrentMap.ToString()}\n");
            _moveLogger.WriteLine($"Previous Map: \n{_mapRenderer.PreviousMap}\n");
            Assert.Fail(ex.ToString());
        }
    }

    private Direction RandomDirection()
    {
        var randomValue = _rng.Next(0, 40);

        if (randomValue >= 0 && randomValue < 10)
            return Direction.Up;
        else if (randomValue > 10 && randomValue < 20)
            return Direction.Right;
        else if (randomValue > 20 && randomValue < 30)
            return Direction.Down;
        else if (randomValue > 30 && randomValue < 40)
            return Direction.Left;

        return Direction.Up;
    }
}