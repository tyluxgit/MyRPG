namespace Engine.Models;

public class ItemQuantity(int itemID, int quantity)
{
    public int ItemID { get; set; } = itemID;
    public int Quantity { get; set; } = quantity;
}