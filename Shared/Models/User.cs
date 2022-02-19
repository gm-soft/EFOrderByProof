using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class User
{
    protected User()
    {
    }

    public User(
        string email)
    {
        Id = Guid.NewGuid();
        Email = email;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; protected set; }

    [Required]
    public string Email { get; protected set; }

    public DateTimeOffset CreatedAt { get; protected set; }
}