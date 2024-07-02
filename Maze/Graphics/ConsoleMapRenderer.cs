namespace Maze.Graphics;

/// <summary>
/// Renders a <see cref="Map"/> into the output of a Console.
/// </summary>
public class ConsoleMapRenderer : IMapRenderer
{
    /// <summary>
    /// A K-V storage of CellType to a string representation of a Map cell, that will be rendered into the console.
    /// </summary>
    private Dictionary<CellType, string> SymbolMap = new Dictionary<CellType, string>()
    {
        { CellType.Empty,  " "  },
        { CellType.Mine,   "*"  },
        { CellType.Player, "P"  },
        { CellType.Exit,   "E"  }
    };
    
    // Refactor too many args
    public void Render(Map map, int lives, int score)
    {
        System.Console.Clear();
        System.Console.WriteLine($"Current score is: {score} | Lives left: {lives}");
        System.Console.WriteLine(CreateChessNotation(map.CurrentPlayerLocation));
        // Unavoidable double for loop
        for (var idx = 0; idx < map.Height; idx++)
        {
            Console.Write("|");
            for (var jdx = 0; jdx < map.Width; jdx++)
            {
                Console.Write("{0}|", SymbolMap[map.GetCell(jdx,idx)]);
            }
            
            Console.WriteLine();
        }
        
        Console.WriteLine(CreateLegend());
    }

    /// <summary>
    /// Renders a chess notation of the players current position.
    /// </summary>
    /// <param name="coordinates">The players coordinates.</param>
    /// <returns>A string that is a formated chess notation.</returns>
    public string CreateChessNotation(Coordinates coordinates)
    {
        var file = ((char)(coordinates.X + (int)'A')).ToString();
        var rank = (coordinates.Y + 1).ToString();
        return $"Player current position: {file}{rank}";
    }

    /// <summary>
    /// Creates a string that is the map legend.
    /// </summary>
    /// <returns>A string.</returns>
    public string CreateLegend()
    {
        return "Legend:\n\t* - Mine\n\tP - Player position\n\tE - Exit";
    }
}