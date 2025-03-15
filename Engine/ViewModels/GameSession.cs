using Engine.Models;
using Engine.Factories;
using PropertyChanged;

namespace Engine.ViewModels;

[AddINotifyPropertyChangedInterface]
public class GameSession
{
    public World CurrentWorld { get; set; }
    public Player CurrentPlayer { get; set; }
    public Location CurrentLocation { get; set; }

    public GameSession()
    {
        CurrentPlayer = new Player
        {
            Name = "Tylux",
            CharacterClass = "Fighter",
            HitPoints = 10,
            Gold = 1000000,
            ExperiencePoints = 0,
            Level = 1
        };
        CurrentWorld = WorldFactory.CreateWorld();
        CurrentLocation = CurrentWorld.LocationAt(0, 0);
    }
    public bool HasLocationToNorth
    {
        get
        {
            return CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;
        }
    }
    public bool HasLocationToEast
    {
        get
        {
            return CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;
        }
    }
    public bool HasLocationToSouth
    {
        get
        {
            return CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;
        }
    }
    public bool HasLocationToWest
    {
        get
        {
            return CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;
        }
    }
    public void MoveNorth()
    {
        if (HasLocationToNorth)
        {
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
        }
    }
    public void MoveEast()
    {
        if (HasLocationToEast)
        {
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
        }
    }
    public void MoveSouth()
    {
        if (HasLocationToSouth)
        {
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
        }
    }
    public void MoveWest()
    {
        if (HasLocationToWest)
        {
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
        }
    }

}

