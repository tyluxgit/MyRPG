using Engine.Models;
namespace Engine.Factories;

public static class TraderFactory
{
    private static readonly List<Trader> _traders = [];
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
    public static Trader? GetTraderByName(string name)
    {
        return _traders.FirstOrDefault(t => t.Name == name);
    }
    private static void AddTraderToList(Trader trader)
    {
        if (_traders.Any(t => t.Name == trader.Name))
        {
            throw new ArgumentException($"There is already a trader named '{trader.Name}'");
        }
        _traders.Add(trader);
    }
}