using Engine.Models;

namespace Engine.Factories;
public static class MonsterFactory
{
    public static Monster GetMonster(int monsterID) => monsterID switch
    {
        1 => CreateMonster(
                name: "Snake",
                imageName: "Snake.png",
                hitPoints: 4,
                minimumDamage: 1,
                maximumDamage: 2,
                rewardExperiencePoints: 5,
                rewardGold: 1,
                lootItems:
                [
                    (9001, 25),
                    (9002, 75)
                ]
             ),
        2 => CreateMonster(
                name: "Rat",
                imageName: "Rat.png",
                hitPoints: 5,
                minimumDamage: 1,
                maximumDamage: 2,
                rewardExperiencePoints: 5,
                rewardGold: 1,
                lootItems:
                [
                    (9003, 25),
                    (9004, 75)
                ]
             ),
        3 => CreateMonster(
                name: "Giant Spider",
                imageName: "Spider.png",
                hitPoints: 10,
                minimumDamage: 2,
                maximumDamage: 4,
                rewardExperiencePoints: 10,
                rewardGold: 3,
                lootItems:
                [
                    (9005, 25),
                    (9006, 75)
                ]
             ),
        _ => throw new ArgumentException($"MonsterType '{monsterID}' does not exist", nameof(monsterID))
    };

    /// <summary>
    /// Crée un monstre avec les paramètres spécifiés et lui ajoute le loot selon les pourcentages définis.
    /// </summary>
    private static Monster CreateMonster(
        string name,
        string imageName,
        int hitPoints,
        int minimumDamage,
        int maximumDamage,
        int rewardExperiencePoints,
        int rewardGold,
        (int itemID, int chance)[] lootItems)
    {
        // Crée le monstre avec les propriétés de base.
        Monster monster = new(name, imageName, hitPoints, minimumDamage, maximumDamage, rewardExperiencePoints, rewardGold);

        // Ajoute les items de loot en fonction des chances.
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
