namespace Engine.ViewModels;
using Engine.Models;

class GameSession
{
    Player CurrentPlayer { get; set; }

    GameSession()
    {
        CurrentPlayer = new Player
        {
            Name = "Tylux",
            CharacterClass = "Fighter",
            Gold = 1000000
        };
    }
}
