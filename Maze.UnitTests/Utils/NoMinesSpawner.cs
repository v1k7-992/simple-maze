using Maze.Spawn;

namespace Maze.UnitTests;

public class NoMinesSpawner : ISpawner
{
    public void SpawnMines(CellType[][] matrix, Random rng)
    {
        for (var idx = 1; idx < matrix.Length; idx++)
        for (var jdx = 0; jdx < matrix[idx].Length; jdx++)
        {
            matrix[idx][jdx] = CellType.Empty;
        }
    }
}