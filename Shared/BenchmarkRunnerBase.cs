using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.OrderingHack;

namespace Shared;

public abstract class BenchmarkRunnerBase
{
    public const int Iterations = 100_000;
    
    [GlobalSetup]
    public async Task Setup()
    {
        await using var context = CreateContext();
        await context.MigrateIfNecessaryAsync();
        if (await context.Users.AnyAsync())
        {
            return;
        }

        var rowsAffected = await context.SaveAsync(new UserCollection(Iterations));
    }

    [Benchmark]
    public async Task<int> RunDefaultAsync()
    {
        await using var context = CreateContext();
        var users = await context.Users.ToListAsync();
        return users.Count;
    }

    [Benchmark]
    public async Task<int> RunWithoutOrderingAsync()
    {
        await using var context = CreateContext();
        var users = await context.Users.RemoveOrdering().ToListAsync();
        return users.Count;
    }
    
    [Benchmark]
    public async Task<int> RunAsSplitAsync()
    {
        await using var context = CreateContext();
        var users = await context.Users.AsSplitQuery().ToListAsync();
        return users.Count;
    }

    protected abstract DatabaseContext CreateContext();
}