namespace risk_api.Models;

public class Building
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Cost { get; set; }
    public BuildingType Type { get; set; }
    public int Income { get; set; }
}


public enum BuildingType
{
    Cash,
    Troop
}