namespace Maze.Spawn;

public class DefaultSpawner : ISpawner
{
    public void SpawnMines(CellType[][] matrix, Random rng)
    {
        for (var idx = 1; idx < matrix.Length; idx++)
        for (var jdx = 0; jdx < matrix[idx].Length; jdx++)
        {
            matrix[idx][jdx] = rng.Next(0, 10) < 5 ? CellType.Empty : CellType.Mine;
        }
    }
}