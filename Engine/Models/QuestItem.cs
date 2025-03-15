namespace Engine.Models;

public class QuestItem(int itemTypeID, string name, int price) : GameItem(itemTypeID, name, price)
{
    public override QuestItem Clone()
    {
        return new QuestItem(ItemTypeID, Name, Price);
    }
}
