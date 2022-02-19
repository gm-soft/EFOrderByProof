namespace Shared.Models;

public class Role
{
    protected Role()
    {
    }

    public Role(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    
    public Guid Id { get; protected set; }

    public string? Name { get; protected set; }

    public virtual ICollection<User> Users { get; protected set; } = new List<User>();
}