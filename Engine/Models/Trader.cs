using PropertyChanged;
using System.Collections.ObjectModel;
namespace Engine.Models;

[AddINotifyPropertyChangedInterface]
public class Trader(string name)
{
    public string Name { get; set; } = name;
    public ObservableCollection<GameItem> Inventory { get; set; } = [];

    public void AddItemToInventory(GameItem item)
    {
        Inventory.Add(item);
    }
    public void RemoveItemFromInventory(GameItem item)
    {
        Inventory.Remove(item);
    }
}