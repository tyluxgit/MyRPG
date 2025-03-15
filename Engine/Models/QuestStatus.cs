namespace Engine.Models;

public class QuestStatus(Quest quest)
{
    public Quest PlayerQuest { get; set; } = quest;
    public bool IsCompleted { get; set; } = false;
}