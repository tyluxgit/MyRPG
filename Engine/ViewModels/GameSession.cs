namespace Engine.ViewModels;
using Engine.Models;

public class GameSession
{
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
        CurrentLocation = new Location()
        {
            Name = "Home",
            XCoordinate = 0,
            YCoordinate = -1,
            Description = "This is your house",
            ImageName = "pack://application:,,,/Engine;component/Images/Locations/Home.png"
        };

    }
}

