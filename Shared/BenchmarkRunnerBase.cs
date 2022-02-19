using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.OrderingHack;

namespace Shared;

public abstract class BenchmarkRunnerBase
{
    public const int Iterations = 10_000;
    
    [GlobalSetup]
    public async Task<int> Setup()
    {
        await using var context = CreateContext();
        await context.MigrateIfNecessaryAsync();
        if (await context.Users.AnyAsync())
        {
            return default;
        }

        var roles = await context.Roles.ToListAsync();
        return await context.SaveAsync(new UserCollection(Iterations, roles));
    }

    [Benchmark]
    public async Task<int> RunDefaultAsync()
    {
        await using var context = CreateContext();
        var users = await context.Users.Include(x => x.Roles).ToListAsync();
        return users.Count;
    }

    [Benchmark]
    public async Task<int> RunWithoutOrderingAsync()
    {
        await using var context = CreateContext();
        var users = await context.Users.Include(x => x.Roles).RemoveOrdering().ToListAsync();
        return users.Count;
    }
    
    [Benchmark]
    public async Task<int> RunAsSplitAsync()
    {
        await using var context = CreateContext();
        var users = await context.Users.Include(x => x.Roles).AsSplitQuery().ToListAsync();
        return users.Count;
    }

    protected abstract DatabaseContext CreateContext();
}