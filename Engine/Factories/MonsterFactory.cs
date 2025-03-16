using Engine.Models;
namespace Engine.Factories;

public static class MonsterFactory
{
    public static Monster GetMonster(int monsterID) => monsterID switch
    {
        1 => CreateMonster("Snake", "Snake.png", 4, 4, 1, 2, 5, 1, lootItems:
            [
                (9001, 25),
                (9002, 75)
            ]),
        2 => CreateMonster("Rat", "Rat.png", 5, 5, 1, 2, 5, 1, lootItems:
            [
                (9003, 25),
                (9004, 75)
            ]),
        3 => CreateMonster("Giant Spider", "Spider.png", 10, 10, 2, 4, 10, 3, lootItems:
            [
                (9005, 25),
                (9006, 75)
            ]),
        _ => throw new ArgumentException($"MonsterType '{monsterID}' does not exist", nameof(monsterID))
    };

    /// <summary>
    /// Crée un monstre avec les paramètres spécifiés et lui ajoute le loot selon les pourcentages définis.
    /// </summary>
    private static Monster CreateMonster(
        string name,
        string imageName,
        int maximumHitPoints,
        int hitPoints,
        int minimumDamage,
        int maximumDamage,
        int rewardExperiencePoints,
        int rewardGold,
        (int itemID, int chance)[] lootItems)
    {
        Monster monster = new(name, imageName, maximumHitPoints, hitPoints, minimumDamage, maximumDamage, rewardExperiencePoints, rewardGold);

        foreach (var (itemID, chance) in lootItems)
        {
            AddLootItem(monster, itemID, chance);
        }

        return monster;
    }

    /// <summary>
    /// Ajoute un item de loot au monstre en fonction du pourcentage de chance.
    /// </summary>
    private static void AddLootItem(Monster monster, int itemID, int percentage)
    {
        if (RandomNumberGenerator.NumberBetween(1, 100) <= percentage)
        {
            monster.Inventory.Add(new ItemQuantity(itemID, 1));
        }
    }
}
