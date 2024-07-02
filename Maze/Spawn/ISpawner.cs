namespace Maze.Spawn;

/// <summary>
/// Used for spawning the game map.
/// </summary>
public interface ISpawner
{
    /// <summary>
    /// Populates a 2D matrix with mines
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="rng"></param>
    void SpawnMines(CellType[][] matrix, Random rng);
}