namespace risk_api.Models;

public class Territory
{
    public string Id { get; set; }
    public string Color { get; set; }
    public Player? ControlledBy { get; set; }
    public DateTime? TimeConquered { get; set; }
    public int Troops { get; set; }
}