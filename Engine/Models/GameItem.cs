namespace Engine.Models;

public abstract class GameItem(int itemTypeID, string name, int price)
{
    public int ItemTypeID { get; set; } = itemTypeID;
    public string Name { get; set; } = name;
    public int Price { get; set; } = price;

    public abstract GameItem Clone();
}
