using risk_api.Controllers.Requests;
using risk_api.DAL.DBContext;
using risk_api.DAL.Processes.DTO;

namespace risk_api.DAL.Processes;

public class TerritoryProcess
{
    private readonly DatabaseContext _context;

    public TerritoryProcess(DatabaseContext context)
    {
        _context = context;
    }

    public IEnumerable<GetAllTerritoriesDto> GetAllTerritories()
    {
        var territories = _context.Territories.ToList();
        var dtoList = new List<GetAllTerritoriesDto>();
        foreach (var territory in territories)
        {
            dtoList.Add(new GetAllTerritoriesDto
            {
                Id = territory.Id,
                Color = territory.Id,
                OwnerId = territory.ControlledBy?.Id,
                TimeConquered = territory.TimeConquered,
                Troops = territory.Troops
            });
        }
        return dtoList;
    }

    public AttackTerritoryDto AttackTerritory(AttackTerritoryRequest request)
    {
        //GATE
        if (request.Troops <= 0) throw new ArgumentException("Cant attack with that amount of troops");
        
        var attackingPlayer = _context.Players.Find(request.AttackingPlayerId);
        if(attackingPlayer == null) throw new KeyNotFoundException("Attacking player does not exist");
        if (attackingPlayer.Troops < request.Troops) throw new ArgumentException("Not enough troops");
        
        var territory = _context.Territories.Find(request.TerritoryId);
        if (territory == null) throw new KeyNotFoundException("Territory does not exist");
        if (territory.ControlledBy?.Id == request.AttackingPlayerId)
            throw new ArgumentException("Cannot attack your own territory");
        //GATE PASSED

        if (request.Troops > territory.Troops) // Attacker win
        {
            attackingPlayer.Troops -= request.Troops;
            territory.Troops = request.Troops - territory.Troops;
            territory.ControlledBy = attackingPlayer;
            territory.Color = attackingPlayer.Color;
            territory.TimeConquered = DateTime.Now;
        }
        else // Attacker lose
        {
            territory.Troops -= request.Troops;
            attackingPlayer.Troops -= request.Troops;
        }

        _context.SaveChanges();

        return new AttackTerritoryDto
        {
            OwnerPlayerId = territory.ControlledBy?.Id,
            TerritoryId = default,
            Troops = territory.Troops,
            Color = territory.Color,
            TimeConquered = territory.TimeConquered
        };
    }

    public ReinforceTerritoryDto ReinforceTerritory(ReinforceTerritoryRequest request)
    {
        //GATE
        if (request.Troops <= 0) throw new ArgumentException("Cant reinforce with that amount of troops");
        
        var reinforcingPlayer = _context.Players.Find(request.ReinforcingPlayerId);
        if(reinforcingPlayer == null) throw new KeyNotFoundException("Player reinforcing does not exist");
        if (reinforcingPlayer.Troops < request.Troops) throw new ArgumentException("Not enough troops");
        
        var territory = _context.Territories.Find(request.TerritoryId);
        if (territory == null) throw new KeyNotFoundException("Territory does not exist");
        //GATE PASSED

        reinforcingPlayer.Troops -= request.Troops;
        territory.Troops += request.Troops;
        _context.SaveChanges();

        return new ReinforceTerritoryDto
        {
            TerritoryId = territory.Id,
            Troops = territory.Troops
        };
    }
}