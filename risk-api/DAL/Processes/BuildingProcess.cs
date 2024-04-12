using risk_api.DAL.DBContext;
using risk_api.DAL.Processes.DTO;
using risk_api.Helpers;
using risk_api.Models;

namespace risk_api.DAL.Processes;

public class BuildingProcess
{
    private readonly DatabaseContext _context;

    public BuildingProcess(DatabaseContext context)
    {
        _context = context;
    }

    public List<GetAllBuildingsDto> GetAllBuildings()
    {
        var buildings = _context.Buildings.ToList();
        var dtoList = new List<GetAllBuildingsDto>();

        foreach (var building in buildings)
        {
            dtoList.Add(new GetAllBuildingsDto
            {
                Id = building.Id,
                Name = building.Name,
                Cost = building.Cost,
                Type = building.Type,
                Income = building.Income
            });
        }

        return dtoList;
    }

    public PurchaseBuildingDto PurchaseBuilding(int buildingId, Guid buyerId)
    {
       var buildingToBuy = _context.Buildings.Find(buildingId);
       if (buildingToBuy == null) throw new GenericClientException(404, "The building to buy does not exist");

       var buyer = _context.Players.Find(buyerId);
       if (buyer == null) throw new GenericClientException(404, "The buyer does not exist. Stop playin with me kid.");

       if (buildingToBuy.Cost > buyer.Cash) throw new GenericClientException(400, "You're broke.");
       
       //Player bought building

       buyer.Cash -= buildingToBuy.Cost;
       
       switch (buildingToBuy.Type)
       {
           case BuildingType.Cash:
               buyer.CashIncome += buildingToBuy.Income;
               break;
           case BuildingType.Troop:
               buyer.TroopIncome += buildingToBuy.Income;
               break;
       }

       _context.SaveChanges();

       return new PurchaseBuildingDto
       {
           Id = buyer.Id,
           Username = buyer.Username,
           Color = buyer.Color,
           Cash = buyer.Cash,
           Troops = buyer.Troops,
           CashIncome = buyer.CashIncome,
           TroopIncome = buyer.TroopIncome,
           Kills = buyer.Kills,
           RerollsLeft = buyer.RerollsLeft
       };
    }
}