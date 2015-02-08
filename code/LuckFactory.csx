#load "ILuckFactory.csx"

public class LuckFactory : ILuckFactory
{
    private IDictionary<int, Luck> luckMap;
    public LuckFactory(IDictionary<int, Luck> luckMap)
    {
        this.luckMap = luckMap;
    }

    public ILuckResult<Luck> Make(ILuckResult<int> kaku)
    {
        var r = new LuckResult<Luck>();
        r.Ten = Convert(kaku.Ten);
        r.Jin = Convert(kaku.Jin);
        r.Ti = Convert(kaku.Ti);
        r.Gai = Convert(kaku.Gai);
        r.Sou = Convert(kaku.Sou);
        r.Syakai = Convert(kaku.Syakai);
        r.Katei = Convert(kaku.Katei);
        r.Nai01 = Convert(kaku.Nai01);
        r.Nai02 = Convert(kaku.Nai02);
        return r;
    }

    private Luck Convert(int p)
    {
        return
            this.luckMap
            .ToMaybe()
            .Where(map => map.ContainsKey(p))
            .Select(map => map[p])
            .Return(Luck.Bad);
    }
}

