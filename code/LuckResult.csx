
public class LuckResult<T> : ILuckResult<T>
{

    public T Ten { get; set; }

    public T Jin { get; set; }

    public T Ti { get; set; }

    public T Gai { get; set; }

    public T Sou { get; set; }

    public T Katei { get; set; }

    public T Syakai { get; set; }

    public T Nai01 { get; set; }

    public T Nai02 { get; set; }

    public IEnumerable<T> ToArray()
    {
        return
            new[]
        {
            this.Gai,
            this.Jin,
            this.Katei,
            this.Nai01,
            this.Nai02,
            this.Sou,
            this.Syakai,
            this.Ten,
            this.Ti,
        };
    }
}

