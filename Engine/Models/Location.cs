namespace Engine.Models;

public class Location
{
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ImageName { get; set; }
    public List<Quest> QuestsAvailableHere { get; set; } = [];
}

