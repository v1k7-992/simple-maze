using Maze.Spawn;

namespace Maze.UnitTests;

public class MapTests
{
    private Map _gameMap;

    private readonly Settings _settings;

    private readonly Random _rng;

    private ISpawner _spawner;

    private IPlayerExitSpawner _playerExitSpawner;
    
    public MapTests()
    {
        _settings = new Settings()
        {
            MapHeight = 10,
            MapWidth = 10,
            NumberOfLives = 10
        };

        _rng = new Random();
        _spawner = new NoMinesSpawner();
        _playerExitSpawner = new DefaultPlayerExitSpawner(_rng);
        _gameMap = new Map(_settings, _spawner, _playerExitSpawner, _rng);
    }
    
    [Fact]
    public void Test_Initialization()
    {
        Assert.Equal(_settings.MapHeight, _gameMap.Height);
        Assert.Equal(_settings.MapWidth, _gameMap.Width);
    }

    [Fact]
    public void Test_PlayerMoves_Y()
    {
        var newCoordinates = _gameMap.CurrentPlayerLocation.Copy();
        newCoordinates.Y += 1;

        var result = _gameMap.SetPlayer(newCoordinates);
        
        Assert.NotEqual(result.OldCoordinates.Y, result.NewCoordinates.Y);
        Assert.Equal(result.OldCoordinates.Y + 1, result.NewCoordinates.Y);
    }
    
    [Fact]
    public void Test_PlayerMoves_X()
    {
        var newCoordinates = _gameMap.CurrentPlayerLocation.Copy();
        var diff = 1;
        
        if (newCoordinates.X < _gameMap.Width -1)
            diff = 1;
        else if (newCoordinates.X == 0)
            diff = -1;

        newCoordinates.X += diff;
        
        var result = _gameMap.SetPlayer(newCoordinates);
        
        Assert.NotEqual(result.OldCoordinates.X, result.NewCoordinates.X);
        Assert.Equal(result.OldCoordinates.X + diff, result.NewCoordinates.X);
    }

    [Fact]
    public void Test_PlayerCanReachExit()
    {
        _playerExitSpawner = new SameColumnPlayerExitSpawner(2);
        _gameMap = new Map(_settings, _spawner, _playerExitSpawner, _rng);

        var result = _gameMap.SetPlayer(_gameMap.ExitLocation.Copy());
        Assert.Equal(_gameMap.ExitLocation, result.NewCoordinates);
    }
    
    [Fact]
    public void Test_PlayerHitMine()
    {
        _playerExitSpawner = new SameColumnPlayerExitSpawner(2);
        _spawner = new AllMinesSpawner();
        _gameMap = new Map(_settings, _spawner, _playerExitSpawner, _rng);

        var result = _gameMap.SetPlayer(new Coordinates(2, 1));
        
        Assert.True(result.DidPlayerHitMine);
    }
}