namespace Maze.Event;

public enum Direction
{
    Up = 0,
    
    Right,
    
    Down,
    
    Left,
    
    None
}

public class PlayerMovementEventArgs(Direction direction = Direction.None) : EventArgs
{
    public Direction Direction { get; private set; } = direction;
}