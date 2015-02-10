
public class NameMapFactoryCached : INameMapFactory
{
    private Lazy<IDictionary<int, IEnumerable<char>>> lazyValue;
    public NameMapFactoryCached(INameMapFactory factory)
    {
        this.lazyValue = new Lazy<IDictionary<int, IEnumerable<char>>>(factory.MakeKakuMap);
    }
    public IDictionary<int, IEnumerable<char>> MakeKakuMap()
    {
        return
            this.lazyValue.Value;
    }
}
