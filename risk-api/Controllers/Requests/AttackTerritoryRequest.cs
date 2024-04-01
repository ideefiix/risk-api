namespace risk_api.Controllers.Requests;

public class AttackTerritoryRequest
{
    public Guid AttackingPlayerId { get; set; }
    public string TerritoryId { get; set; }
    public int Troops { get; set; }
}