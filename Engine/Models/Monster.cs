using System.Collections.ObjectModel;
using PropertyChanged;

namespace Engine.Models;
[AddINotifyPropertyChangedInterface]
public class Monster(string name, string imageName,
    int maximumHitPoints, int hitPoints,
    int rewardExperiencePoints, int rewardGold)
{
    public string Name { get; private set; } = name;
    public string ImageName { get; set; } = string.Format("pack://application:,,,/Engine;component/Images/Monsters/{0}", imageName);
    public int MaximumHitPoints { get; private set; } = maximumHitPoints;
    public int HitPoints { get; private set; } = hitPoints;
    public int RewardExperiencePoints { get; private set; } = rewardExperiencePoints;
    public int RewardGold { get; private set; } = rewardGold;
    public ObservableCollection<ItemQuantity> Inventory { get; set; } = [];
}