using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using Shared.Docker;
using Shared.Models;
using Shared.OrderingHack;

namespace Shared;

public class Benchmarker<TContainer> : IAsyncDisposable
where TContainer: IDatabaseContainer, new()
{
    public const int Iterations = 1_000;

    private bool _disposed;
    private IDatabaseContainer? _databaseContainer;
    private DatabaseContext? _context;
    
    [GlobalSetup]
    public async Task<int> Setup()
    {
        _databaseContainer = new TContainer();
        await _databaseContainer.StartAsync();
        _context = _databaseContainer.GetContext();

        if (await _context.Users!.AnyAsync())
        {
            return default;
        }

        var roles = await _context.Roles!.ToListAsync();
        return await _context.SaveAsync(new UserCollection(Iterations, roles));
    }

    [Benchmark]
    public async Task<int> RunDefaultAsync()
    {
        var users = await _context!.Users!.Include(x => x.Roles).ToListAsync();
        return users.Count;
    }

    [Benchmark]
    public async Task<int> RunWithoutOrderingAsync()
    {
        var users = await _context!.Users!.Include(x => x.Roles).RemoveOrdering().ToListAsync();
        return users.Count;
    }
    
    [Benchmark]
    public async Task<int> RunAsSplitAsync()
    {
        var users = await _context!.Users!.Include(x => x.Roles).AsSplitQuery().ToListAsync();
        return users.Count;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        if (_databaseContainer is not null)
        {
            await _databaseContainer.DisposeAsync();
        }

        _disposed = true;
    }
}