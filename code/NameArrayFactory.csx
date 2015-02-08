#load "INameArrayFactory.csx"

public class NameArrayFactory : INameArrayFactory
{
    private INameMapFactory nameMapFactory;
    public NameArrayFactory(INameMapFactory nameMapFactory)
    {
        this.nameMapFactory = nameMapFactory;
    }
    public Luck Make(string firstName, string lastName)
    {
        var map = this.nameMapFactory.MakeKakuMap();
        var tenList =
            from c in lastName
            from clist in map
            where clist.Value.Contains(c)
            let num = clist.Key
            select num % 2 == 0;

        var tiList =
            from c in firstName
            from clist in map
            where clist.Value.Contains(c)
            let num = clist.Key
            select num % 2 == 0;

        var maybeGood =
            from _ in 0.ToMaybe()
            where tenList.Last() != tiList.First()
            let sumTen = tenList.Select(x => x ? 1 : 0).Sum()
            let sumTi = tiList.Select(x => x ? 1 : 0).Sum()
            let isSameAllTen =
                sumTen == 0
                || sumTen == tenList.Count()
            let isSameAllTi =
                sumTi == 0
                || sumTi == tiList.Count()
            where (isSameAllTen & isSameAllTi) == false
            select Luck.Good;

        return
            maybeGood.Return(Luck.Bad);
    }
}
