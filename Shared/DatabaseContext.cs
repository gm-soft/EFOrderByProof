using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Shared;

public abstract class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
}