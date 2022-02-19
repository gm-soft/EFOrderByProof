using Microsoft.EntityFrameworkCore;
using Shared;

namespace SqlServerApp;

public class SqlServerDatabaseContext : DatabaseContext
{
    private readonly string _connectionString;

    public SqlServerDatabaseContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost; Database=eopa; User Id=sa; Password=Str0ngPass!;");
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