namespace Maze;

/// <summary>
/// Represents the contents of a cell inside a map.
/// </summary>
public enum CellType
{
    /// <summary>
    /// Empty field
    /// </summary>
    Empty = 0,

    /// <summary>
    /// Field with a mine
    /// </summary>
    Mine = 1,
    
    /// <summary>
    /// The player
    /// </summary>
    Player = 2,
    
    /// <summary>
    /// The map exit
    /// </summary>
    Exit = 3,
    
    /// <summary>
    /// A cell type that does not exsist.
    /// </summary>
    Null = 4
}
