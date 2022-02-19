using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Shared;

public abstract class DatabaseContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    
    public DbSet<Role>? Roles { get; set; }

    public async Task<int> SaveAsync(
        IEnumerable<User> users)
    {
        await Users!.AddRangeAsync(users);
        return await SaveChangesAsync();
    }

    public abstract Task MigrateIfNecessaryAsync();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Query.Name });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .HasMany(x => x.Roles)
            .WithMany(x => x.Users);

        modelBuilder
            .Entity<Role>()
            .HasData(new Role("Employee"), new Role("Admin"));

        base.OnModelCreating(modelBuilder);
    }
}