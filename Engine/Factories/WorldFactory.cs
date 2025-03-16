using Engine.Models;

namespace Engine.Factories;

internal static class WorldFactory
{
    internal static World CreateWorld()
    {
        World newWorld = new();

        // Farmer's Field (-2, -1)
        newWorld.AddLocation(
            -2, -1,
            "Farmer's Field",
            "There are rows of corn growing here, with giant rats hiding between them.",
            "FarmFields.jpg");
        var farmersField = newWorld.LocationAt(-2, -1);
        farmersField?.AddMonster(2, 100);

        // Farmer's House (-1, -1)
        newWorld.AddLocation(
            -1, -1,
            "Farmer's House",
            "This is the house of your neighbor, Farmer Ted.",
            "Farmhouse.jpg");

        // Home (0, -1)
        newWorld.AddLocation(
            0, -1,
            "Home",
            "This is your home",
            "Home.png");

        // Trading Shop (-1, 0)
        newWorld.AddLocation(
            -1, 0,
            "Trading Shop",
            "The shop of Susan, the trader.",
            "Trader.jpg");

        // Town square (0, 0)
        newWorld.AddLocation(
            0, 0,
            "Town square",
            "You see a fountain here.",
            "TownSquare.jpg");

        // Town Gate (1, 0)
        newWorld.AddLocation(
            1, 0,
            "Town Gate",
            "There is a gate here, protecting the town from giant spiders.",
            "TownGate.jpg");

        // Spider Forest (2, 0)
        newWorld.AddLocation(
            2, 0,
            "Spider Forest",
            "The trees in this forest are covered with spider webs.",
            "SpiderForest.jpg");
        var spiderForest = newWorld.LocationAt(2, 0);
        spiderForest?.AddMonster(3, 100);

        // Herbalist's hut (0, 1)
        newWorld.AddLocation(
            0, 1,
            "Herbalist's hut",
            "You see a small hut, with plants drying from the roof.",
            "HerbalistsHut.jpg");
        var herbalistsHut = newWorld.LocationAt(0, 1);
        herbalistsHut?.QuestsAvailableHere.Add(QuestFactory.GetQuestByID(1));

        // Herbalist's garden (0, 2)
        newWorld.AddLocation(
            0, 2,
            "Herbalist's garden",
            "There are many plants here, with snakes hiding behind them.",
            "HerbalistsGarden.jpg");
        var herbalistsGarden = newWorld.LocationAt(0, 2);
        herbalistsGarden?.AddMonster(1, 100);

        return newWorld;
    }
}
