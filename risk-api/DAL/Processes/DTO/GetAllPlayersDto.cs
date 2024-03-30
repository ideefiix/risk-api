namespace risk_api.DAL.Processes.DTO;

public class GetAllPlayersDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Color { get; set; }
    public int Kills { get; set; }
}