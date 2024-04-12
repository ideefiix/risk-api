namespace risk_api.Controllers.Requests;

public class ReinforceTerritoryRequest
{
    public Guid ReinforcingPlayerId { get; set; }
    public string TerritoryId { get; set; }
    public int Troops { get; set; }
}