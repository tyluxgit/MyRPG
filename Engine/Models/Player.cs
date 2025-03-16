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
    public void RemoveItemFromInventory(GameItem item)
    {
        Inventory.Remove(item);
    }
    public bool HasAllTheseItems(List<ItemQuantity> items)
    {
        foreach (ItemQuantity item in items)
        {
            if (Inventory.Count(i => i.ItemTypeID == item.ItemID) < item.Quantity)
            {
                return false;
            }
        }
        return true;
    }
}

