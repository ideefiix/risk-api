namespace risk_api.DAL.Processes.DTO;

public class GetAllTerritoriesDto
{
    public string Id { get; set; }
    public string Color { get; set; }
    public Guid? OwnerId { get; set; }
    public DateTime? TimeConquered { get; set; }
    public int Troops { get; set; }
}