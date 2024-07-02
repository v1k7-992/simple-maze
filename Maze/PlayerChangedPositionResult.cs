namespace Maze;

public class PlayerChangedPositionResult
{
    public Coordinates OldCoordinates;

    public Coordinates NewCoordinates;

    public bool DidPlayerHitMine;

    public bool DidPlayerMove => OldCoordinates != NewCoordinates;

    public PlayerChangedPositionResult(Coordinates oldCoordinates, Coordinates newCoordinates, bool didPlayerHitMine)
    {
        OldCoordinates = oldCoordinates;
        NewCoordinates = newCoordinates;
        DidPlayerHitMine = didPlayerHitMine;
    }
}