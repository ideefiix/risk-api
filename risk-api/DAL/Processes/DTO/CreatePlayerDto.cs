namespace risk_api.DAL.Processes.DTO;

public class CreatePlayerDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Color { get; set; }
    public int Cash { get; set; }
    public int Troops { get; set; }
    public int CashIncome { get; set; }
    public int TroopIncome { get; set; }
    public int Kills { get; set; }
    public int RerollsLeft { get; set; }
}