using Microsoft.EntityFrameworkCore;
using risk_api.Controllers.Requests;
using risk_api.DAL.DBContext;
using risk_api.DAL.Processes.DTO;
using risk_api.Helpers;

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
        var territories = _context.Territories.Include(t => t.ControlledBy).ToList();
        var dtoList = new List<GetAllTerritoriesDto>();
        foreach (var territory in territories)
        {
            dtoList.Add(new GetAllTerritoriesDto
            {
                Id = territory.Id,
                Color = territory.Color,
                OwnerId = territory.ControlledBy?.Id,
                Ownername = territory.ControlledBy?.Username,
                TimeConquered = territory.TimeConquered,
                Troops = territory.Troops
            });
        }
        return dtoList;
    }

    public AttackTerritoryDto AttackTerritory(AttackTerritoryRequest request)
    {
        //GATE
        if (request.Troops <= 0) throw new GenericClientException(400, "Cant attack with that amount of troops");

        var attackingPlayer = _context.Players.Find(request.AttackingPlayerId);
        if (attackingPlayer == null) throw new GenericClientException(404, "Attacking player does not exist"); 
        if (attackingPlayer.Troops < request.Troops) throw new GenericClientException(400, "Not enough troops");
        
        var territory = _context.Territories.Find(request.TerritoryId);
        if (territory == null) throw new GenericClientException(404, "Territory does not exist");
        if (territory.ControlledBy?.Id == request.AttackingPlayerId)
            throw new GenericClientException(400, "Cannot attack your own territory");
        //GATE PASSED

        if (request.Troops > territory.Troops) // Attacker win
        {
            attackingPlayer.Troops -= request.Troops;
            attackingPlayer.Kills += territory.Troops;
            territory.Troops = request.Troops - territory.Troops;
            territory.ControlledBy = attackingPlayer;
            territory.Color = attackingPlayer.Color;
            territory.TimeConquered = DateTime.UtcNow;
        }
        else // Attacker lose
        {
            territory.Troops -= request.Troops;
            attackingPlayer.Troops -= request.Troops;
            attackingPlayer.Kills += request.Troops; //Each attacker kill 1 defender
        }

        _context.SaveChanges();

        return new AttackTerritoryDto
        {
            Id = territory.Id,
            Color = territory.Color,
            OwnerId = territory.ControlledBy?.Id,
            Ownername = territory.ControlledBy?.Username,
            TimeConquered = territory.TimeConquered,
            Troops = territory.Troops
        };
    }

    public ReinforceTerritoryDto ReinforceTerritory(ReinforceTerritoryRequest request)
    {
        //GATE
        if (request.Troops <= 0) throw new GenericClientException(400, "Cant reinforce with that amount of troops"); 
        
        var reinforcingPlayer = _context.Players.Find(request.ReinforcingPlayerId);
        if (reinforcingPlayer == null) throw new GenericClientException(404, "Player reinforcing does not exist");
        if (reinforcingPlayer.Troops < request.Troops) throw new GenericClientException(400, "Not enough troops");
        
        var territory = _context.Territories.Find(request.TerritoryId);
        if (territory == null) throw new GenericClientException(404, "Territory does not exist");
        //GATE PASSED

        reinforcingPlayer.Troops -= request.Troops;
        territory.Troops += request.Troops;
        _context.SaveChanges();

        return new ReinforceTerritoryDto
        {
            Id = territory.Id,
            Color = territory.Color,
            OwnerId = territory.ControlledBy?.Id,
            Ownername = territory.ControlledBy?.Username,
            TimeConquered = territory.TimeConquered,
            Troops = territory.Troops
        };
    }
}