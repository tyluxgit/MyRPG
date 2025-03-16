using System.Collections.ObjectModel;
using PropertyChanged;

namespace Engine.Models;

[AddINotifyPropertyChangedInterface]
public class Monster(string name, string imageName, 
               int hitPoints,
               int minimumDamage, int maximumDamage,
               int rewardExperiencePoints, int rewardGold)
{
    public string Name { get; private set; } = name;
    public string ImageName { get; set; } = $"pack://application:,,,/Engine;component/Images/Monsters/{imageName}";
    public int MinimumDamage { get; set; } = minimumDamage;
    public int MaximumDamage { get; set; } = maximumDamage;
    public int HitPoints { get; set; } = hitPoints;
    public int RewardExperiencePoints { get; private set; } = rewardExperiencePoints;
    public int RewardGold { get; private set; } = rewardGold;
    public ObservableCollection<ItemQuantity> Inventory { get; set; } = [];
}
