using Engine.Models;
using Engine.Factories;
using PropertyChanged;
using Engine.EventArgs;

namespace Engine.ViewModels;

[AddINotifyPropertyChangedInterface]
public class GameSession
{
    #region Events
    public event EventHandler<GameMessageEventArgs>? OnMessageRaised;
    #endregion
    #region Properties
    public World CurrentWorld { get; private set; }
    public Player CurrentPlayer { get; private set; }
    public Location CurrentLocation { get; set; }
    public Monster? CurrentMonster { get; set; }
    public Weapon? CurrentWeapon { get; set; }
    public Trader? CurrentTrader { get; set; }
    public void MoveNorth() => Move(Direction.North);
    public void MoveEast() => Move(Direction.East);
    public void MoveSouth() => Move(Direction.South);
    public void MoveWest() => Move(Direction.West);

    private Location? _northLocation;
    private Location? _eastLocation;
    private Location? _southLocation;
    private Location? _westLocation;
    public Monster? _currentMonster;
    public Trader? _currentTrader;

    #endregion
    #region Game Session Constructor
    public GameSession()
    {
        CurrentPlayer = new Player
        {
            Name = "Tylux",
            CharacterClass = "Fighter",
            HitPoints = 10,
            Gold = 0,
            ExperiencePoints = 0,
            Level = 1,
        };
        
        CurrentWorld = WorldFactory.CreateWorld()
            ?? throw new InvalidOperationException("World creation failed");

        CurrentLocation = CurrentWorld.LocationAt(0, -1)
            ?? throw new InvalidOperationException("Starting location invalid");

        if (CurrentPlayer.Weapons.Count == 0)
        {
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
        }
        CombatService.OnMessageRaised += RaiseMessage;


    }
    #endregion
    #region Location Properties
    [DependsOn(nameof(CurrentLocation))]
    public bool HasLocationToNorth => (_northLocation ??= GetAdjacentLocation(0, 1)) is not null;
    [DependsOn(nameof(CurrentLocation))]
    public bool HasLocationToEast => (_eastLocation ??= GetAdjacentLocation(1, 0)) is not null;
    [DependsOn(nameof(CurrentLocation))]
    public bool HasLocationToSouth => (_southLocation ??= GetAdjacentLocation(0, -1)) is not null;
    [DependsOn(nameof(CurrentLocation))]
    public bool HasLocationToWest => (_westLocation ??= GetAdjacentLocation(-1, 0)) is not null;
    public bool HasMonster => CurrentMonster != null;
    public bool HasTrader => CurrentTrader != null;
    #endregion
    #region Game Actions
    private Location? GetAdjacentLocation(int deltaX, int deltaY)
    {
        if (CurrentWorld is null || CurrentLocation is null)
        {
            return null;
        }

        return CurrentWorld.LocationAt(
            CurrentLocation.XCoordinate + deltaX,
            CurrentLocation.YCoordinate + deltaY
        );
    }

    public void Move(Direction direction)
    {
        var (dx, dy) = direction switch
        {
            Direction.North => (0, 1),
            Direction.East => (1, 0),
            Direction.South => (0, -1),
            Direction.West => (-1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };

        var newLocation = GetAdjacentLocation(dx, dy);
        if (newLocation != null)
        {
            CurrentLocation = newLocation;
            CurrentTrader = CurrentLocation?.TraderHere;
            ResetAdjacentLocationsCache();
            GivePlayerQuestsAtLocation();
            GetMonsterAtLocation();
            CompleteQuestsAtLocation();
        }
    }
    #endregion
    #region Location Methods
    private void ResetAdjacentLocationsCache()
    {
        _northLocation = _eastLocation = _southLocation = _westLocation = null;
    }

    private void GivePlayerQuestsAtLocation()
    {
        foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
        {
            if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
            {
                CurrentPlayer.Quests.Add(new QuestStatus(quest));

                // Fonction helper pour formater les objets requis
                static string FormatItemQuantity(ItemQuantity itemQuantity)
                {
                    var item = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                    return $"{itemQuantity.Quantity}x {item.Name}";
                }

                // Formatage des objectifs
                string requirements = quest.ItemsToComplete.Count != 0
                    ? string.Join("\n- ", quest.ItemsToComplete.Select(FormatItemQuantity))
                    : "No required items";

                // Formatage des récompenses
                string itemRewards = quest.RewardItems.Count != 0
                    ? string.Join("\n- ", quest.RewardItems.Select(FormatItemQuantity))
                    : "No item reward";

                // Construction du message
                RaiseMessage(
                    $"New quest : {quest.Name}\n" +
                    $"Description : {quest.Description}\n\n" +
                    $"Objectives :\n- {requirements}\n\n" +
                    $"Rewards :\n" +
                    $"- XP : {quest.RewardExperiencePoints}\n" +
                    $"- Gold : {quest.RewardGold}\n" +
                    $"- {itemRewards}");
            }
        }
    }
    private void CompleteQuestsAtLocation()
    {
        foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
        {
            QuestStatus? questToComplete =
                CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID & !q.IsCompleted);
            if (questToComplete != null)
            {
                if (CurrentPlayer.HasAllTheseItems(quest.ItemsToComplete))
                {
                    // Remove the quest completion items from the player's inventory
                    foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        for (int i = 0; i < itemQuantity.Quantity; i++)
                        {
                            CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(item => item.ItemTypeID == itemQuantity.ItemID));
                        }
                    }
                    RaiseMessage("");
                    RaiseMessage($"You completed the '{quest.Name}' quest");
                    // Give the player the quest rewards
                    CurrentPlayer.ExperiencePoints += quest.RewardExperiencePoints;
                    RaiseMessage($"You receive {quest.RewardExperiencePoints} experience points");
                    CurrentPlayer.Gold += quest.RewardGold;
                    RaiseMessage($"You receive {quest.RewardGold} gold");
                    foreach (ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        GameItem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                        CurrentPlayer.AddItemToInventory(rewardItem);
                        RaiseMessage($"You receive a {rewardItem.Name}");
                    }
                    // Mark the Quest as completed
                    questToComplete.IsCompleted = true;
                }
            }
        }
    }
    public void GetMonsterAtLocation()
    {
        CurrentMonster = CurrentLocation.GetMonster();
        if (CurrentMonster is not null)
            RaiseMessage($"\nYou see a {CurrentMonster.Name} here!");
    }
    #endregion
    public void AttackCurrentMonster()
    {
        CombatService.AttackMonster(this);
    }
    #region Game Messages
    public void RaiseMessage(object sender, GameMessageEventArgs e)
    {
        OnMessageRaised?.Invoke(sender, e);
    }
    public void RaiseMessage(string message)
    {
        RaiseMessage(this, new GameMessageEventArgs(message));
    }


    #endregion
}
