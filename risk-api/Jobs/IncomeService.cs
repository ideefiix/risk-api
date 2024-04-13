using risk_api.DAL.Processes;

namespace risk_api.Jobs;

public class IncomeService : BackgroundService
{
    private readonly PeriodicTimer _timer = new PeriodicTimer(TimeSpan.FromMinutes(5));
    private IServiceProvider Services { get; } 
    
    public IncomeService(IServiceProvider services)
    {
        Services = services;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("Triggering income!");
            await GiveIncome();
        }
    }

    private async Task GiveIncome()
    {
        using (var scope = Services.CreateScope())
        {
            var process =
                scope.ServiceProvider
                    .GetRequiredService<ServiceProcess>();
            await process.GiveIncomeToPlayers();
        }
    }
    
}