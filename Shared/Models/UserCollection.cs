using System.Collections;
using System.Text;

namespace Shared.Models;

public class UserCollection : IEnumerable<User>
{
    private readonly int _count;

    public UserCollection(int count)
    {
        _count = count;
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
            yield return new User(builder.ToString());
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}