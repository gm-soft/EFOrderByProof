using Microsoft.EntityFrameworkCore;
using Shared;

namespace PostgreApp;

public class PostgreDbContext : DatabaseContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=eopa;User Id=pi;Password=Str0ngPass!");
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