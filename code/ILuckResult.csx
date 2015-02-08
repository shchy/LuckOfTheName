

public interface ILuckResult<T>
{
    T Ten { get; }
    T Jin { get; }
    T Ti { get; }
    T Gai { get; }
    T Sou { get; }
    T Katei { get; }
    T Syakai { get; }
    T Nai01 { get; }
    T Nai02 { get; }

    IEnumerable<T> ToArray();
}

