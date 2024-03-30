namespace risk_api.Models;

public class Player
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Color { get; set; }
    public int Cash { get; set; }
    public int Troops { get; set; }
    public int CashIncome { get; set; }
    public int TroopIncome { get; set; }
    public int Kills { get; set; }
    public int RerollsLeft { get; set; }
}