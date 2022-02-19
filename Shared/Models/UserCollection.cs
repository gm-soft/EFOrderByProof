using System.Collections;
using System.Text;

namespace Shared.Models;

public class UserCollection : IEnumerable<User>
{
    private readonly int _count;
    private readonly IList<Role> _roles;

    public UserCollection(int count, IList<Role> roles)
    {
        _count = count;
        _roles = roles;
    }

    public IEnumerator<User> GetEnumerator()
    {
        for (var i = 0; i < _count; i++)
        {
            var builder = new StringBuilder();
            builder.Append(Faker.Name.First());
            builder.Append(".");
            builder.Append(Faker.Name.Last());
            builder.Append("@example.com");

            var role = _roles[i % 2];
            yield return new User(builder.ToString(), role);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}