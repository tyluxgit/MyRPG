﻿using Engine.Models;

namespace Engine.Factories;

internal static class QuestFactory
{
    private static readonly Dictionary<int, Quest> _quests = new();
    internal static IReadOnlyDictionary<int, Quest> Quests => _quests;

    // Static constructor to initialize quests
    static QuestFactory()
    {
        // Create and add the quest to the dictionary
        AddQuest(1, "Clear the herb garden", "Defeat the snakes in the Herbalist's garden",
                 new List<ItemQuantity> { new(9001, 5) }, 25, 10, new List<ItemQuantity> { new(1002, 1) });
    }

    // Adds a quest to the dictionary if no quest with the same ID exists
    private static void AddQuest(int id, string name, string description,
                                 List<ItemQuantity> itemsRequired, int expReward,
                                 int goldReward, List<ItemQuantity> rewardItems)
    {
        // Ensure required lists are not null
        if (itemsRequired == null) throw new ArgumentNullException(nameof(itemsRequired));
        if (rewardItems == null) throw new ArgumentNullException(nameof(rewardItems));

        if (!_quests.TryAdd(id, new Quest(id, name, description, itemsRequired, expReward, goldReward, rewardItems)))
        {
            throw new InvalidOperationException($"Quest with ID {id} already exists in the quest list.");
        }
    }

    // Retrieve a quest by its ID
    internal static Quest GetQuestByID(int id)
    {
        if (_quests.TryGetValue(id, out Quest quest))
        {
            return quest;
        }
        throw new KeyNotFoundException($"Quest with ID {id} not found in the quest dictionary.");
    }
}
