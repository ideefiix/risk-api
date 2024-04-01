namespace risk_api.DAL.Processes.DTO;

public class AttackTerritoryDto
{
    public Guid? OwnerPlayerId { get; set; }
    public Guid TerritoryId { get; set; }
    public int Troops { get; set; }
    public string Color { get; set; }
    public DateTime? TimeConquered { get; set; }
}