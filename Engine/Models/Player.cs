using PropertyChanged;
using System.Collections.ObjectModel;
namespace Engine.Models;

[AddINotifyPropertyChangedInterface]
public class Player
{
    #region Properties
    public required string Name { get; set; }
    public required string CharacterClass { get; set; }
    public int HitPoints { get; set; }
    public int ExperiencePoints { get; set; }
    public int Level { get; set; }
    public int Gold { get; set; }
    public ObservableCollection<GameItem> Inventory { get; set; } = [];
    public List<GameItem> Weapons => [.. Inventory.Where(i => i is Weapon)];
    public ObservableCollection<QuestStatus> Quests { get; set; } = [];
    #endregion
    public void AddItemToInventory(GameItem item)
    {
        Inventory.Add(item);
    }
}

