using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Shared;

public abstract class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public async Task<int> SaveAsync(
        IEnumerable<User> users)
    {
        await Users.AddRangeAsync(users);
        return await SaveChangesAsync();
    }

    public abstract Task MigrateIfNecessaryAsync();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Query.Name });
    }
}