using Engine.Models;

namespace Engine.ViewModels;

public static class TradeService
{
    public static void SellItem(Player player, Trader trader, GameItem item)
    {
        if (player == null || trader == null || item == null) return;

        player.Gold += item.Price;
        trader.AddItemToInventory(item);
        player.RemoveItemFromInventory(item);
    }

    public static bool BuyItem(Player player, Trader trader, GameItem item)
    {
        if (player == null || trader == null || item == null) return false;

        if (player.Gold >= item.Price)
        {
            player.Gold -= item.Price;
            trader.RemoveItemFromInventory(item);
            player.AddItemToInventory(item);
            return true;
        }

        return false; // Achat échoué
    }
}
