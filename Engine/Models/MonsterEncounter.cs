namespace Engine.Models;

public class MonsterEncounter(int monsterID, int chanceOfEncountering)
{
    public int MonsterID { get; set; } = monsterID;
    public int ChanceOfEncountering { get; set; } = chanceOfEncountering;
}