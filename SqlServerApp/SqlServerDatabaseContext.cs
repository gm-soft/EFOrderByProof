using Microsoft.EntityFrameworkCore;
using Shared;

namespace SqlServerApp;

public class SqlServerDatabaseContext : DatabaseContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost; Database=eopa; User Id=sa; Password=Str0ngPass!;");
    }
}