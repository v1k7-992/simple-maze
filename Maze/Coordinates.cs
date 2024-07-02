namespace Maze;

/// <summary>
/// Represents the coordinates in X and Y axis.
/// </summary>
public class Coordinates
{
    protected bool Equals(Coordinates other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Coordinates)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(Coordinates current, Coordinates other)
    {
        return current.Equals(other);
    }
    
    public static bool operator !=(Coordinates current, Coordinates other)
    {
        return !current.Equals(other);
    }
    
    public int X { get; set; }
    
    public int Y { get; set; }

    public Coordinates()
    {
        X = 0;
        Y = 0;
    }
    
    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Coordinates Copy()
    {
        return new Coordinates(X, Y);
    }
}