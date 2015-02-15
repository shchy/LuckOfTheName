#load "code/Luck.csx"
#load "code/ILuckMapFactory.csx"
#load "code/LuckMapFactory.csx"
#load "code/IKakuFactory.csx"
#load "code/KakuFactory.csx"
#load "code/ILuckFactory.csx"
#load "code/LuckFactory.csx"
#load "code/ILuckResult.csx"
#load "code/LuckResult.csx"
#load "code/ILuckView.csx"
#load "code/LuckView.csx"
#load "code/IName.csx"
#load "code/Name.csx"
#load "code/INameFactory.csx"
#load "code/NameFactory.csx"
#load "code/INameArrayFactory.csx"
#load "code/NameArrayFactory.csx"
#load "code/INameMapFactory.csx"
#load "code/NameMapFactory.csx"
#load "code/NameMapFactoryCached.csx"


// args
var lastNameArg = Env.ScriptArgs[0];
var _badCount = 0;
var isInBadCount = 
    from m in Env.ScriptArgs.ToMaybe()
    where m.Count() > 1
    let a = m[1]
    where int.TryParse(a, out _badCount)
    select Fn.New((int bc) => bc <= _badCount);
var _rank = 0;
var isInRank =
    from m in Env.ScriptArgs.ToMaybe()
    where m.Count() > 2
    let a = m[2]
    where int.TryParse(a, out _rank)
    select Fn.New((int r) => r >= _rank);




var nameMapFactory =
        new NameMapFactoryCached(
            new NameMapFactory());
var nameArrayFactory = new NameArrayFactory(nameMapFactory);
var nameFactory = new NameFactory();
var kakuFactory = new KakuFactory(nameMapFactory);
var luckMapFactory = new LuckMapFactory();
var fiveLuckFactory = new LuckFactory(luckMapFactory.Make());
var fiveLuckView = new LuckView();
 
 
var sampleList =
    from a in nameMapFactory.MakeKakuMap()
    let sample = a.Value.First()
    select new { a.Key, sample };
 
var xs =
    from a in sampleList
    from b in sampleList
    let firstname = a.sample.ToString() + b.sample.ToString()
    let lastName = lastNameArg
    let name = nameFactory.Make(firstname, lastName)
    let arrayLuck = nameArrayFactory.Make(firstname, lastName)
    where arrayLuck != Luck.Bad
    let kaku = kakuFactory.Make(name)
    let lucks = fiveLuckFactory.Make(kaku)
    let badCount = lucks.ToArray().Count(x=>x== Luck.Bad)
    where isInBadCount.Select(f=> f(badCount)).Return(true)
    let sum = lucks.ToArray().Sum(x => (int)x)
    where isInRank.Select(f => f(sum)).Return(true)
    orderby sum descending
    select new
    {
        name00 = a.Key,
        name01 = b.Key,
        lucks,
        sum,
    };
 
 
Write("result.txt", w =>
    {
        foreach (var item in xs)
        {
            w.WriteLine("-----------------------------------");
            w.WriteLine(item.sum);
            w.WriteLine(fiveLuckView.View(item.lucks));
            w.WriteLine("{0} {1}", item.name00, item.name01);
            

            var n00 =
                from a in nameMapFactory.MakeKakuMap()
                where a.Key == item.name00
                from v in a.Value
                select v;
 
            var n01 =
                from a in nameMapFactory.MakeKakuMap()
                where a.Key == item.name01
                from v in a.Value
                select v;
 
            //var count = Math.Max(n00.Count(), n01.Count());
            //for (int i = 0; i < count; i++)
            //{
            //    var x00 = n00.ElementAtOrDefault(i).ToMaybe().Where(c=>c!=0).Return(' ');
            //    var x01 = n01.ElementAtOrDefault(i).ToMaybe().Where(c=>c!=0).Return(' ');
            //    w.WriteLine("{0} {1} {2}",test, x00, x01);
            //}
 
            var names =
                from x in n00
                from y in n01
                select lastNameArg + " " + x.ToString() + y.ToString();
            names.ForEach(w.WriteLine);
        }
    });
 
void Write(string filename, Action<StreamWriter> write)
{
    using (var ms = new MemoryStream())
    using (var w = new StreamWriter(ms))
    using (var fs = new FileStream(filename, FileMode.Create, FileAccess.Write ))
    using (var fw = new BinaryWriter(fs))
    {
        write(w);
        w.Flush();
        fw.Write(ms.ToArray());
    }
}
