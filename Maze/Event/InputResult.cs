namespace Maze.Event;

/// <summary>
/// Represents the result of a game loop.
/// </summary>
/// <param name="gameState">The current state of the game.</param>
public record InputResult(GameState gameState)
{
    public GameState GameState { get; set; } = gameState;
}