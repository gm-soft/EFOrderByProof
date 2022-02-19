namespace Shared.Docker;

public interface IDatabaseContainer : IAsyncDisposable
{
    Task StartAsync();

    DatabaseContext GetContext();
}