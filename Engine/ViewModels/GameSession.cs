namespace Engine.ViewModels;
using Engine.Models;
public class GameSession
{
    public Player CurrentPlayer { get; set; }

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
    }
}
