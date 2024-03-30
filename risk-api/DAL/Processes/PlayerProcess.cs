using Microsoft.AspNetCore.Identity;
using risk_api.Controllers.Requests;
using risk_api.DAL.DBContext;
using risk_api.DAL.Processes.DTO;
using risk_api.Helpers;
using risk_api.Models;

namespace risk_api.DAL.Processes;

public class PlayerProcess
{
    private readonly DatabaseContext _context;

    public PlayerProcess(DatabaseContext context)
    {
        _context = context;
    }

    public IEnumerable<GetAllPlayersDto> GetAllPlayers()
    {
        var players = _context.Players.ToList();
        var Dtos = new List<GetAllPlayersDto>();

        foreach (var player in players)
        {
            Dtos.Add(MapGetallPlayersDto(player));
        }

        return Dtos;
    }
    
    public GetPlayerDto GetSinglePlayer(Guid playerId)
    {
        var player = _context.Players.Find(playerId);
        if (player == null)
        {
            throw new KeyNotFoundException("A player with that Id does not exist");
        }
        
        return MapGetSinglePlayerDto(player);
    }

    public CreatePlayerDto CreatePlayer(CreatePlayerRequest request)
    {
        var usernameIsTaken = _context.Players.FirstOrDefault(p => p.Username == request.Username);

        if (usernameIsTaken != null) throw new ArgumentException("Username is already taken");

        var hasher = new PasswordHasher<Player>();
        var p = new Player
        {
            Username = request.Username,
            Color = ColorGenerator.GetRandomColour(),
            Cash = 100,
            Troops = 100,
            CashIncome = 10,
            TroopIncome = 10,
            Kills = 0,
            RerollsLeft = 3
        };
        p.Password = hasher.HashPassword(p, request.Password);

        _context.Players.Add(p);
        _context.SaveChanges();
        return MapCreatePlayerDto(p);
    }

    private CreatePlayerDto MapCreatePlayerDto(Player p)
    {
        return new CreatePlayerDto
        {
            Id = p.Id,
            Username = p.Username,
            Color = p.Color,
            Cash = p.Cash,
            Troops = p.Troops,
            CashIncome = p.CashIncome,
            TroopIncome = p.TroopIncome,
            Kills = p.Kills,
            RerollsLeft = p.RerollsLeft
        };
    }
    private GetPlayerDto MapGetSinglePlayerDto(Player p)
    {
        return new GetPlayerDto
        {
            Id = p.Id,
            Username = p.Username,
            Color = p.Color,
            Cash = p.Cash,
            Troops = p.Troops,
            CashIncome = p.CashIncome,
            TroopIncome = p.TroopIncome,
            Kills = p.Kills,
            RerollsLeft = p.RerollsLeft
        };
    } 
   private GetAllPlayersDto MapGetallPlayersDto(Player p)
   {
       return new GetAllPlayersDto
       {
           Id = p.Id,
           Username = p.Username,
           Color = p.Color,
           Kills = p.Kills
       };
   } 
}

