using Engine.Models;

namespace Engine.Factories;

internal static class QuestFactory
{
    // Initialisation de la collection des quêtes
    private static readonly Dictionary<int, Quest> _quests = [];
    internal static IReadOnlyDictionary<int, Quest> Quests => _quests;

    // Constructeur statique pour initialiser les quêtes
    static QuestFactory()
    {
        // Création et ajout d'une quête dans le dictionnaire
        AddQuest(
            id: 1,
            name: "Clear the herb garden",
            description: "Defeat the snakes in the Herbalist's garden",
            itemsRequired: [new ItemQuantity(9001, 5)],
            expReward: 25,
            goldReward: 10,
            rewardItems: [new ItemQuantity(1002, 1)]
        );
    }

    /// <summary>
    /// Ajoute une quête dans le dictionnaire, en s'assurant qu'il n'existe pas déjà une quête avec le même ID.
    /// </summary>
    private static void AddQuest(
        int id,
        string name,
        string description,
        List<ItemQuantity> itemsRequired,
        int expReward,
        int goldReward,
        List<ItemQuantity> rewardItems)
    {
        // Vérification que les listes requises ne sont pas nulles
        ArgumentNullException.ThrowIfNull(itemsRequired);
        ArgumentNullException.ThrowIfNull(rewardItems);

        // Tentative d'ajout de la quête dans le dictionnaire
        if (!_quests.TryAdd(id, new Quest(id, name, description, itemsRequired, expReward, goldReward, rewardItems)))
        {
            throw new InvalidOperationException($"Quest with ID {id} already exists in the quest list.");
        }
    }

    /// <summary>
    /// Retourne la quête correspondant à l'ID spécifié.
    /// </summary>
    internal static Quest GetQuestByID(int id)
    {
        if (_quests.TryGetValue(id, out Quest quest))
        {
            return quest;
        }
        throw new KeyNotFoundException($"Quest with ID {id} not found in the quest dictionary.");
    }
}
