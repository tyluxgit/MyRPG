using PropertyChanged;
using System.Collections.ObjectModel;
namespace Engine.Models;

[AddINotifyPropertyChangedInterface]
public class Player
{
    public required string Name { get; set; }
    public required string CharacterClass { get; set; }
    public int HitPoints { get; set; }
    public int ExperiencePoints { get; set; }
    public int Level { get; set; }
    public int Gold { get; set; }
    public ObservableCollection<GameItem> Inventory { get; set; } = [];
    public ObservableCollection<QuestStatus> Quests { get; set; } = [];

}

