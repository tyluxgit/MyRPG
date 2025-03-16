using Engine.Factories;

namespace Engine.Models;

public class Location
{
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ImageName { get; set; }
    public List<Quest> QuestsAvailableHere { get; set; } = [];
    public List<MonsterEncounter> MonstersHere { get; set; } = [];
    public Trader TraderHere { get; set; }

    /// <summary>
    /// Ajoute un monstre à la liste des monstres disponibles ou met à jour la chance d'apparition si le monstre existe déjà.
    /// </summary>
    public void AddMonster(int monsterID, int chanceOfEncountering)
    {
        var existingMonster = MonstersHere.FirstOrDefault(m => m.MonsterID == monsterID);
        if (existingMonster != null)
        {
            // Mise à jour de la chance si le monstre existe déjà
            existingMonster.ChanceOfEncountering = chanceOfEncountering;
        }
        else
        {
            // Ajout du monstre s'il n'existe pas encore à cet emplacement
            MonstersHere.Add(new MonsterEncounter(monsterID, chanceOfEncountering));
        }
    }

    /// <summary>
    /// Sélectionne un monstre aléatoirement parmi ceux disponibles, en fonction de leur chance d'apparition.
    /// Renvoie null s'il n'y a aucun monstre.
    /// </summary>
    public Monster? GetMonster()
    {
        if (MonstersHere.Count == 0)
            return null;

        // Totaliser les pourcentages de tous les monstres disponibles.
        int totalChances = MonstersHere.Sum(m => m.ChanceOfEncountering);

        // Sélectionner un nombre aléatoire entre 1 et le total des chances.
        int randomNumber = RandomNumberGenerator.NumberBetween(1, totalChances);

        // Parcourir la liste en cumulant les chances et retourner le monstre dès que le total dépasse le nombre aléatoire.
        int runningTotal = 0;
        foreach (var monsterEncounter in MonstersHere)
        {
            runningTotal += monsterEncounter.ChanceOfEncountering;
            if (randomNumber <= runningTotal)
                return MonsterFactory.GetMonster(monsterEncounter.MonsterID);
        }

        // En cas d'erreur (ce qui ne devrait pas arriver), retourner le dernier monstre de la liste.
        return MonsterFactory.GetMonster(MonstersHere.Last().MonsterID);
    }
}
