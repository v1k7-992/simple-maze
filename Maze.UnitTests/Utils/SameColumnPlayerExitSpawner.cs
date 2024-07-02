using Maze.Spawn;

namespace Maze.UnitTests;

public class SameColumnPlayerExitSpawner : IPlayerExitSpawner
{
    private int _column;

    public SameColumnPlayerExitSpawner(int column)
    {
        _column = column;
    }

    public Coordinates SpawnPlayer(CellType[][] map)
    {
        return new Coordinates(_column, 0);
    }

    public Coordinates SpawnExit(CellType[][] map)
    {
        return new Coordinates(_column, map.Length - 1);
    }
}