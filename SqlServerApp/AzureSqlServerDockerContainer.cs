using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules.Abstractions;
using Shared;
using Shared.Docker;

namespace SqlServerApp;

public class AzureSqlServerDockerContainer : IDatabaseContainer
{
    private readonly TestcontainerDatabase _dbContainer;
    private bool _disposed;
    private SqlServerDatabaseContext? _context;

    public AzureSqlServerDockerContainer()
    {
        _dbContainer = new TestcontainersBuilder<TestcontainerDatabase>()
            .WithName("azure-sql-database")
            .WithImage("mcr.microsoft.com/azure-sql-edge")
            .WithPortBinding(1433, 1433)
            .WithEnvironment("SA_PASSWORD", "Str0ngPass!")
            .WithEnvironment("ACCEPT_EULA", "Y")
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