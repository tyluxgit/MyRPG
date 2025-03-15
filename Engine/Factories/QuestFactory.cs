using Engine.Models;

namespace Engine.Factories
{
    internal static class QuestFactory
    {
        private static readonly Dictionary<int, Quest> _quests = [];
        internal static IReadOnlyDictionary<int, Quest> Quests => _quests;


        // Static constructor to initialize quests
        static QuestFactory()
        {
            // Create and add the quest to the dictionary
            AddQuest(1, "Clear the herb garden", "Defeat the snakes in the Herbalist's garden",[new(9001, 5)],25, 10, [new(1002, 1)]);

        }

        private static void AddQuest(int id, string name, string description,
                             List<ItemQuantity> itemsRequired, int expReward, int goldReward,
                             List<ItemQuantity> rewardItems)
        {
            if (!_quests.TryAdd(id, new Quest(id, name, description, itemsRequired, expReward, goldReward, rewardItems)))
            {
                throw new InvalidOperationException($"A quest with ID {id} already exists.");
            }

        }

        // Retrieve a quest by its ID
        internal static Quest GetQuestByID(int id)
        {
            if (_quests.TryGetValue(id, out Quest quest))
            {
                return quest;
            }
            throw new KeyNotFoundException($"No quest found with ID {id}");

        }
    }
}
