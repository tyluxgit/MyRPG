namespace Engine.Models;

public class World
{
    private readonly Dictionary<(int, int), Location> _locations = [];

    public void AddLocation(int xCoordinate, int yCoordinate, string name, string description, string imageName)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));

        var key = (xCoordinate, yCoordinate);
        if (_locations.ContainsKey(key))
            throw new ArgumentException($"Location at ({xCoordinate}, {yCoordinate}) already exists");

        Location loc = new()
        {
            XCoordinate = xCoordinate,
            YCoordinate = yCoordinate,
            Name = name,
            Description = description,
            ImageName = $"/Engine;component/Images/Locations/{imageName}"
        };
        _locations.Add(key, loc);
    }

    public Location? LocationAt(int xCoordinate, int yCoordinate)
    {
        var key = (xCoordinate, yCoordinate);
        return _locations.TryGetValue(key, out var location) ? location : null;
    }

    public bool TryGetLocation(int xCoordinate, int yCoordinate, out Location? location)
    {
        return _locations.TryGetValue((xCoordinate, yCoordinate), out location);
    }
}
