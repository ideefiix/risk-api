namespace risk_api.Controllers.Requests;

public class ReinforceTerritoryRequest
{
    public Guid ReinforcingPlayerId { get; set; }
    public Guid TerritoryId { get; set; }
    public int Troops { get; set; }
}