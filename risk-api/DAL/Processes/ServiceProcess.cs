using Microsoft.EntityFrameworkCore;
using risk_api.DAL.DBContext;

namespace risk_api.DAL.Processes;

public class ServiceProcess
{
    private readonly DatabaseContext _context;

    public ServiceProcess(DatabaseContext context)
    {
        _context = context;
    }

    public Task GiveIncomeToPlayers()
    {
        var sql = @"Update ""Players"" SET ""Cash"" = ""Cash"" + ""CashIncome"", ""Troops"" = ""Troops"" + ""TroopIncome""";
  
        _context.Database.ExecuteSqlRaw(sql);
        return _context.SaveChangesAsync();
    }
}