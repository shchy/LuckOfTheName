
public class KakuFactory : IKakuFactory
{
    private INameMapFactory nameMapFactory;
    public KakuFactory(INameMapFactory nameMapFactory)
    {
        this.nameMapFactory = nameMapFactory;
    }

    public ILuckResult<int> Make(IName name)
    {
        var r = new LuckResult<int>();
        r.Ten = name.LastName.Select(ToKakuCount).Sum();
        r.Jin =
            ToKakuCount(name.LastName.Last())
            + ToKakuCount(name.FirstName.First());
        r.Ti = name.FirstName.Select(ToKakuCount).Sum();
        r.Sou = r.Ten + r.Ti;
        r.Gai = r.Sou - r.Jin;
        r.Katei = r.Jin + name.FirstName.Skip(1).Select(ToKakuCount).Sum();
        r.Syakai = r.Jin + name.LastName.Reverse().Skip(1).Select(ToKakuCount).Sum();
        r.Nai01 = ToKakuCount(name.FirstName.First()) + ToKakuCount(name.LastName.First());
        r.Nai02 = r.Sou - r.Nai01;
        return r;
    }

    private int ToKakuCount(char c)
    {
        return
            this.nameMapFactory.MakeKakuMap()
            .Where(clist => clist.Value.Contains(c))
            .Select(x => x.Key)
            .FirstOrNothing()
            .Return(0);
    }
}

