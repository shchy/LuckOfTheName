#load "code/Luck.csx"
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


var nameMapFactory =
        new NameMapFactoryCached(
            new NameMapFactory());
var nameArrayFactory = new NameArrayFactory(nameMapFactory);
var nameFactory = new NameFactory();
var kakuFactory = new KakuFactory(nameMapFactory);
var fiveLuckFactory = new LuckFactory(MakeLuckMap());
var fiveLuckView = new LuckView();
 
 
var sampleList =
    from a in nameMapFactory.MakeKakuMap()
    let sample = a.Value.First()
    select new { a.Key, sample };
 
var test = "横田";
var xs =
    from a in sampleList
    from b in sampleList
    where b.Key != 1
    let firstname = a.sample.ToString() + b.sample.ToString()
    let lastName = test
    let name = nameFactory.Make(firstname, lastName)
    let arrayLuck = nameArrayFactory.Make(firstname, lastName)
    where arrayLuck != Luck.Bad
    let kaku = kakuFactory.Make(name)
    let lucks = fiveLuckFactory.Make(kaku)
    //let isNotBad =
    //    lucks.Jin != Luck.Bad
    //    && lucks.Ti != Luck.Bad
    //    && lucks.Gai != Luck.Bad
    //    && lucks.Sou != Luck.Bad
    //    && lucks.Katei != Luck.Bad
    //    && lucks.Syakai != Luck.Bad
    //    && lucks.Nai01 != Luck.Bad
    //    && lucks.Nai02 != Luck.Bad
    //where isNotBad
    let badCount = lucks.ToArray().Count(x=>x== Luck.Bad)
    where badCount == 2
    let sum = lucks.ToArray().Sum(x => (int)x)
    //where sum > 13
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
 
            var count = Math.Max(n00.Count(), n01.Count());
            for (int i = 0; i < count; i++)
            {
                var x00 = n00.ElementAtOrDefault(i);
                var x01 = n01.ElementAtOrDefault(i);
                w.WriteLine("{0} {1} {2}",test, x00, x01);
            }
 
            //names.ForEach(w.WriteLine);
        }
    });
Write("list.txt", w =>
{
    foreach (var item in xs)
    {
        w.WriteLine("-----------------------------------");
        w.WriteLine(item.sum);
        w.WriteLine(fiveLuckView.View(item.lucks));
 
        var names =
            from a in nameMapFactory.MakeKakuMap()
            from b in nameMapFactory.MakeKakuMap()
            where a.Key == item.name00
            where b.Key == item.name01
            from v in a.Value
            from v2 in b.Value
            select v.ToString() + v2.ToString();
 
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

IDictionary<int, Luck> MakeLuckMap()
{
    return
        new Dictionary<int, Luck>
        {
            { 11, Luck.Best },
            { 16, Luck.Best },
            { 21, Luck.Best },
            { 23, Luck.Best },
            { 31, Luck.Best },
            { 32, Luck.Best },
            { 41, Luck.Best },

            { 3, Luck.Better },
            { 5, Luck.Better },
            { 6, Luck.Better },
            { 8, Luck.Better },
            { 13, Luck.Better },
            { 15, Luck.Better },
            { 18, Luck.Better },
            { 24, Luck.Better },
            { 25, Luck.Better },
            { 29, Luck.Better },
            { 33, Luck.Better },
            { 37, Luck.Better },
            { 39, Luck.Better },
            { 44, Luck.Better },
            { 45, Luck.Better },
            { 47, Luck.Better },
            { 48, Luck.Better },
            { 51, Luck.Better },
            { 52, Luck.Better },

            { 7, Luck.Good },
            { 17, Luck.Good },
            { 27, Luck.Good },
            { 30, Luck.Good },
            { 34, Luck.Good },
            { 35, Luck.Good },
            { 36, Luck.Good },
            { 38, Luck.Good },
            { 40, Luck.Good },
            { 42, Luck.Good },
            { 43, Luck.Good },
            { 49, Luck.Good },
            { 53, Luck.Good },
            { 57, Luck.Good },
            { 58, Luck.Good },
        };
}