using System.Text;
using Maze.Graphics;
using Xunit.Abstractions;

namespace Maze.UnitTests;

public class DebugRenderer : IMapRenderer
{
    public string PreviousMap;
    
    public StringBuilder CurrentMap;

    public DebugRenderer()
    {
        CurrentMap = new StringBuilder();
    }
    
    private Dictionary<CellType, string> SymbolMap = new Dictionary<CellType, string>()
    {
        { CellType.Empty,  " "  },
        { CellType.Mine,   "*"  },
        { CellType.Player, "P"  },
        { CellType.Exit,   "E"  }
    };
    
    public void Render(Map map, int lives, int score)
    {
        PreviousMap = CurrentMap.ToString();
        CurrentMap.Clear();
        for (var idx = 0; idx < map.Height; idx++)
        {
            CurrentMap.Append("|");
            for (var jdx = 0; jdx < map.Width; jdx++)
            {
                CurrentMap.AppendFormat("{0}|", SymbolMap[map.GetCell(jdx,idx)]);
            }
            
            CurrentMap.AppendLine();
        }
    }
}