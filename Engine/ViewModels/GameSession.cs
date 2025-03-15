using Engine.Models;
using Engine.Factories;

namespace Engine.ViewModels;
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
        WorldFactory factory = new();
        CurrentWorld = factory.CreateWorld();
        CurrentLocation = CurrentWorld.LocationAt(0, -1);
    }
}

