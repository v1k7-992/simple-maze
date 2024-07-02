namespace Maze.Spawn;

public class DefaultPlayerExitSpawner : IPlayerExitSpawner
{
    private readonly Random _rng;

    public DefaultPlayerExitSpawner(Random rng)
    {
        _rng = rng;
    }

    public Coordinates SpawnPlayer(CellType[][] matrix)
    {
        return new Coordinates(_rng.Next(0, matrix[0].Length), 0);
    }

    public Coordinates SpawnExit(CellType[][] matrix)
    {
        return new Coordinates(_rng.Next(0, matrix[^1].Length), matrix.Length - 1);
    }
}