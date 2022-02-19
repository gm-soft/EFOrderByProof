using Microsoft.EntityFrameworkCore;
using Shared;

namespace PostgreApp;

public class PostgreDbContext : DatabaseContext
{
    private readonly string _connectionString;

    public PostgreDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        base.OnConfiguring(optionsBuilder);
    }

    public override async Task MigrateIfNecessaryAsync()
    {
        if ((await Database.GetPendingMigrationsAsync()).Any())
        {
            await Database.MigrateAsync();
        }
    }
}