using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Configurations.Databases;
using DotNet.Testcontainers.Containers.Modules;
using DotNet.Testcontainers.Containers.Modules.Abstractions;
using DotNet.Testcontainers.Containers.Modules.Databases;
using DotNet.Testcontainers.Containers.WaitStrategies;
using Shared;
using Shared.Docker;

namespace SqlServerApp;

public class AzureSqlServerDockerContainer : IDatabaseContainer
{
    private readonly MsSqlTestcontainer _dbContainer;
    private bool _disposed;
    private SqlServerDatabaseContext? _context;

    public AzureSqlServerDockerContainer()
    {
        _dbContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(new MsSqlTestcontainerConfiguration("mcr.microsoft.com/azure-sql-edge")
            {
                Password = "Str0ngPass!"
            })
            .Build();
    }

    public async Task StartAsync()
    {
        await _dbContainer.StartAsync();
        _context = new SqlServerDatabaseContext(_dbContainer.ConnectionString);
        await _context.MigrateIfNecessaryAsync();
    }

    public DatabaseContext GetContext()
    {
        return _context ?? throw new InvalidOperationException();
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        await _dbContainer.DisposeAsync();
        
        if (_context is not null)
        {
            await _context.DisposeAsync();
        }

        _disposed = true;
    }
}