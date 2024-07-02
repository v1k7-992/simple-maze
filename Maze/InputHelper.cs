namespace Maze;

public class InputHelper
{
    /// <summary>
    /// Tests if the user wants to quit the program.
    /// </summary>
    /// <param name="key">Currently inputed key</param>
    /// <returns>True if user pressed a key that exits the game. Otherwise, false.</returns>
    public static bool IsUserRequestingGameExit(ConsoleKeyInfo key)
    {
        return key.Key is ConsoleKey.Q or ConsoleKey.Escape;
    }
}