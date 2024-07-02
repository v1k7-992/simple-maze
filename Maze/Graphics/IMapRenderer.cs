namespace Maze.Graphics;

/// <summary>
/// Interface to an implementation, used for rendering games graphics.
/// </summary>
public interface IMapRenderer
{
    /// <summary>
    /// Renders a map.
    /// </summary>
    /// <param name="map">A map.</param>
    /// <param name="lives">Current number of lives the player has.</param>
    /// <param name="score">The players score.</param>
    void Render(Map map, int lives, int score);
}