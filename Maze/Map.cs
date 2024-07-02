using Maze.Spawn;

namespace Maze;

/// <summary>
/// Represents a data structure that servers as a board of a 2D game map.
/// </summary>
public class Map
{
    /// <summary>
    /// Internal map data which is a 2D matrix, where first array is Y coordinates and the second array is the X
    /// coordinates.
    /// </summary>
    private readonly CellType[][] Matrix;

    /// <summary>
    /// The map height
    /// </summary>
    public int Height { get; }
    
    /// <summary>
    /// The map width
    /// </summary>
    public int Width { get; }
    
    /// <summary>
    /// Internal tracker where the current player position is.
    /// </summary>
    public Coordinates CurrentPlayerLocation { get; private set; }
    
    /// <summary>
    /// Internal tracker where the exit location is.
    /// </summary>
    public Coordinates ExitLocation { get;  }
    
    public Map(Settings settings, ISpawner spawner, IPlayerExitSpawner playerExitSpawner, Random rng)
    {
        Width = settings.MapWidth;
        Height = settings.MapHeight;
        
        Matrix = new CellType[Height][];
        for (var idx = 0; idx < Matrix.Length; idx++)
            Matrix[idx] = new CellType[Width];
        
        CurrentPlayerLocation = playerExitSpawner.SpawnPlayer(Matrix);
        
        // mine spawn should be fairer to the player, provide a safe path to the exit using maze generation algo.
        spawner.SpawnMines(Matrix, rng);

        Matrix[CurrentPlayerLocation.Y][CurrentPlayerLocation.X] = CellType.Player;

        ExitLocation = playerExitSpawner.SpawnExit(Matrix);
        Matrix[ExitLocation.Y][ExitLocation.X] = CellType.Exit;
    }

    public CellType? GetCell(Coordinates coordinates)
    {
        if (CheckBounds(coordinates))
        {
            return Matrix[coordinates.Y][coordinates.X];
        }

        return null;
    }
    
    public CellType GetCell(int x, int y)
    {
        if (CheckBounds(x, y))
        {
            return Matrix[y][x];
        }

        return CellType.Null;
    }
    
    public PlayerChangedPositionResult SetPlayer(Coordinates coordinates)
    {
        var previousCoordinates = CurrentPlayerLocation.Copy();
        if (coordinates.X >= 0 && coordinates.X < Width ||
            coordinates.Y >= 0 && coordinates.Y < Height)
        {
            CurrentPlayerLocation = coordinates;
        }

        var didPlayerHitMine = Matrix[CurrentPlayerLocation.Y][CurrentPlayerLocation.X] == CellType.Mine;

        if (CurrentPlayerLocation != previousCoordinates)
        {
            Matrix[CurrentPlayerLocation.Y][CurrentPlayerLocation.X] = CellType.Player;
            Matrix[previousCoordinates.Y][previousCoordinates.X] = CellType.Empty;
        }

        return new PlayerChangedPositionResult(
                previousCoordinates,
                CurrentPlayerLocation,
                didPlayerHitMine                
            );
    }

    /// <summary>
    /// Checks if coordinates are within map bounds.
    /// </summary>
    /// <param name="coordinates">Proveded coordinates</param>
    /// <returns>True if within map bounds, otherwise false.</returns>
    private bool CheckBounds(Coordinates coordinates)
    {
        return ((coordinates.X >= 0 && coordinates.X < Width) &&
                (coordinates.Y >= 0 && coordinates.Y < Height));
    }
    
    /// <summary>
    /// Checks if coordinates are within map bounds.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <returns>True if within map bounds, otherwise false.</returns>
    private bool CheckBounds(int x, int y)
    {
        return ((x >= 0 && x < Width) &&
                (y >= 0 && y < Height));
    }
}