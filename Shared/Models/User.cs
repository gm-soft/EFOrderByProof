using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class User
{
    protected User()
    {
    }

    public User(
        string email,
        Role role)
    {
        Id = Guid.NewGuid();
        Email = email;
        CreatedAt = DateTimeOffset.UtcNow;
        Roles = new List<Role>();
        Roles.Add(role);
    }

    public Guid Id { get; protected set; }

    [Required]
    public string? Email { get; protected set; }

    public DateTimeOffset CreatedAt { get; protected set; }

    public virtual ICollection<Role> Roles { get; protected set; } = new List<Role>();
}