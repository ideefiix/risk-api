using Microsoft.EntityFrameworkCore;
using risk_api.Models;

namespace risk_api.DAL.DBContext;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Territory> Territories { get; set; } = null!;
    
}