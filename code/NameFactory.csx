
public class NameFactory : INameFactory
{
    public IName Make(string firstName, string lastName)
    {
        return
            new Name
            {
                FirstName = firstName,
                LastName = lastName,
            };
    }
}

