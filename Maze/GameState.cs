namespace Maze;

/// <summary>
/// Enumeration that represents the current state the game is.
/// </summary>
public enum GameState
{
    /// <summary>
    /// This state means player can move.
    /// </summary>
    PlayerAlive = 1,
    
    /// <summary>
    /// This state means the player cannot move, and has lost the match.
    /// </summary>
    PlayerDead,
    
    /// <summary>
    /// This state means the player has won the match.
    /// </summary>
    PlayerIsAtExit
}