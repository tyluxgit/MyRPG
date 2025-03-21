using Engine.Models;

namespace Engine.Factories;

public static class TraderFactory
{
    // Liste centralisée des traders
    private static readonly List<Trader> _traders = [];

    // Constructeur statique : initialise les traders
    static TraderFactory()
    {
        Trader susan = new("Susan");
        susan.AddItemToInventory(ItemFactory.CreateGameItem(1001));

        Trader farmerTed = new("Farmer Ted");
        farmerTed.AddItemToInventory(ItemFactory.CreateGameItem(1001));

        Trader peteTheHerbalist = new("Pete the Herbalist");
        peteTheHerbalist.AddItemToInventory(ItemFactory.CreateGameItem(1001));

        AddTraderToList(susan);
        AddTraderToList(farmerTed);
        AddTraderToList(peteTheHerbalist);
    }

    /// <summary>
    /// Retourne le trader correspondant au nom donné.
    /// </summary>
    /// <param name="name">Nom du trader recherché</param>
    /// <returns>Trader correspondant ou null s’il n’existe pas</returns>
    public static Trader? GetTraderByName(string name)
    {
        return _traders.FirstOrDefault(t => t.Name == name);
    }

    /// <summary>
    /// Ajoute un trader à la liste des traders, en vérifiant l’unicité par nom.
    /// </summary>
    /// <param name="trader">Trader à ajouter</param>
    private static void AddTraderToList(Trader trader)
    {
        if (_traders.Any(t => t.Name == trader.Name))
        {
            throw new ArgumentException($"There is already a trader named '{trader.Name}'");
        }
        _traders.Add(trader);
    }
}
