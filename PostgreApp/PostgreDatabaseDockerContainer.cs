using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Configurations.Databases;
using DotNet.Testcontainers.Containers.Modules.Abstractions;
using DotNet.Testcontainers.Containers.Modules.Databases;
using Shared;
using Shared.Docker;

namespace PostgreApp;

public class PostgreDatabaseDockerContainer : IDatabaseContainer
{
    private readonly TestcontainerDatabase _dbContainer;
    private bool _disposed;
    private PostgreDbContext? _context;

    public PostgreDatabaseDockerContainer()
    {
        _dbContainer = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "eopa",
                Username = "pi",
                Password = "Str0ngPass!",
            })
            .Build();
    }

    public async Task StartAsync()
    {
        await _dbContainer.StartAsync();
        _context = new PostgreDbContext(_dbContainer.ConnectionString);
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