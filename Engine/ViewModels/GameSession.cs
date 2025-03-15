using Engine.Models;
using Engine.Factories;
using PropertyChanged;
using System.Collections.ObjectModel;

namespace Engine.ViewModels;

[AddINotifyPropertyChangedInterface]
public class GameSession
{
    public World CurrentWorld { get; private set; }
    public Player CurrentPlayer { get; private set; }
    public Location CurrentLocation { get; private set; }
    public ObservableCollection<GameItem> Inventory { get; }

    public GameSession()
    {
        Inventory = [];
        CurrentPlayer = new Player(Inventory)
        {
            Name = "Tylux",
            CharacterClass = "Fighter",
            HitPoints = 10,
            Gold = 1_000_000,
            ExperiencePoints = 0,
            Level = 1
        };

        CurrentWorld = WorldFactory.CreateWorld()
            ?? throw new InvalidOperationException("World creation failed");

        CurrentLocation = CurrentWorld.LocationAt(0, -1)
            ?? throw new InvalidOperationException("Starting location invalid");
    }

    [DependsOn(nameof(CurrentLocation))]
    public bool HasLocationToNorth => GetAdjacentLocation(0, 1) != null;

    [DependsOn(nameof(CurrentLocation))]
    public bool HasLocationToEast => GetAdjacentLocation(1, 0) != null;

    [DependsOn(nameof(CurrentLocation))]
    public bool HasLocationToSouth => GetAdjacentLocation(0, -1) != null;

    [DependsOn(nameof(CurrentLocation))]
    public bool HasLocationToWest => GetAdjacentLocation(-1, 0) != null;

    private Location GetAdjacentLocation(int deltaX, int deltaY)
    {
        return CurrentWorld.LocationAt(
            CurrentLocation.XCoordinate + deltaX,
            CurrentLocation.YCoordinate + deltaY
        );
    }

    public void Move(Direction direction)
    {
        var (dx, dy) = direction switch
        {
            Direction.North => (0, 1),
            Direction.East => (1, 0),
            Direction.South => (0, -1),
            Direction.West => (-1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };

        var newLocation = GetAdjacentLocation(dx, dy);
        if (newLocation != null)
        {
            CurrentLocation = newLocation;
        }
    }
}

public enum Direction { North, East, South, West }
