using Engine.Models;

namespace Engine.Factories;

internal static class WorldFactory
{
    internal static World CreateWorld()
    {
        World newWorld = new();

        // Farmer's Field (-2, -1)
        AddLocationWithMonster(
            newWorld, -2, -1,
            "Farmer's Field",
            "There are rows of corn growing here, with giant rats hiding between them.",
            "FarmFields.jpg",
            monsterID: 2, monsterEncounterChance: 100);

        // Farmer's House (-1, -1)
        AddLocationWithTrader(
            newWorld, -1, -1,
            "Farmer's House",
            "This is the house of your neighbor, Farmer Ted.",
            "Farmhouse.jpg",
            traderName: "Farmer Ted");

        // Home (0, -1)
        AddLocation(
            newWorld, 0, -1,
            "Home",
            "This is your home",
            "Home.png");

        // Trading Shop (-1, 0)
        AddLocationWithTrader(
            newWorld, -1, 0,
            "Trading Shop",
            "The shop of Susan, the trader.",
            "Trader.jpg",
            traderName: "Susan");

        // Town square (0, 0)
        AddLocation(
            newWorld, 0, 0,
            "Town square",
            "You see a fountain here.",
            "TownSquare.jpg");

        // Town Gate (1, 0)
        AddLocation(
            newWorld, 1, 0,
            "Town Gate",
            "There is a gate here, protecting the town from giant spiders.",
            "TownGate.jpg");

        // Spider Forest (2, 0)
        AddLocationWithMonster(
            newWorld, 2, 0,
            "Spider Forest",
            "The trees in this forest are covered with spider webs.",
            "SpiderForest.jpg",
            monsterID: 3, monsterEncounterChance: 100);

        // Herbalist's hut (0, 1)
        var herbalistsHut = AddLocationWithTrader(
            newWorld, 0, 1,
            "Herbalist's hut",
            "You see a small hut, with plants drying from the roof.",
            "HerbalistsHut.jpg",
            traderName: "Pete the Herbalist");
        herbalistsHut?.QuestsAvailableHere.Add(QuestFactory.GetQuestByID(1));

        // Herbalist's garden (0, 2)
        AddLocationWithMonster(
            newWorld, 0, 2,
            "Herbalist's garden",
            "There are many plants here, with snakes hiding behind them.",
            "HerbalistsGarden.jpg",
            monsterID: 1, monsterEncounterChance: 100);

        return newWorld;
    }

    private static Location? AddLocation(
        World world, int xCoordinate, int yCoordinate,
        string name, string description, string imageName)
    {
        world.AddLocation(xCoordinate, yCoordinate, name, description, imageName);
        return world.LocationAt(xCoordinate, yCoordinate);
    }

    private static Location? AddLocationWithTrader(
        World world, int xCoordinate, int yCoordinate,
        string name, string description, string imageName, string traderName)
    {
        var location = AddLocation(world, xCoordinate, yCoordinate, name, description, imageName);
        if (location != null)
        {
            location.TraderHere = TraderFactory.GetTraderByName(traderName);
        }
        return location;
    }

    private static Location? AddLocationWithMonster(
        World world, int xCoordinate, int yCoordinate,
        string name, string description, string imageName,
        int monsterID, int monsterEncounterChance)
    {
        var location = AddLocation(world, xCoordinate, yCoordinate, name, description, imageName);
        location?.AddMonster(monsterID, monsterEncounterChance);
        return location;
    }
}