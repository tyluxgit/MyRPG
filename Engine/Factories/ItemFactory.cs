using Engine.Models;
namespace Engine.Factories;

public static class ItemFactory
{
    private static readonly List<GameItem> _standardGameItems;
    static ItemFactory()
    {
        _standardGameItems =
            [
                new Weapon(1001, "Pointy Stick", 1, 1, 2),
                new Weapon(1002, "Rusty Sword", 5, 1, 3),
                new QuestItem(9001, "Snake fang", 1),
                new QuestItem(9002, "Snakeskin", 2),
                new QuestItem(9003, "Rat tail", 1),
                new QuestItem(9004, "Rat fur", 2),
                new QuestItem(9005, "Spider fang", 1),
                new QuestItem(9006, "Spider silk", 2),
            ];
    }
    public static GameItem CreateGameItem(int itemTypeID)
    {
        GameItem? standardItem = _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID);

        return standardItem switch
        {
            Weapon weapon => weapon.Clone(),
            QuestItem questItem => questItem.Clone(),
            _ => throw new ArgumentException($"Aucun élément de type {itemTypeID} n'existe.", nameof(itemTypeID)),
        };
    }

}