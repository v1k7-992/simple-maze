namespace Maze.Spawn;

// should probably just pass width and height?
public interface IPlayerExitSpawner
{
    Coordinates SpawnPlayer(CellType[][] map);

    Coordinates SpawnExit(CellType[][] map);
}