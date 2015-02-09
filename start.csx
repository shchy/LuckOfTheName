﻿#load "code/Luck.csx"
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
var luckMapFactory = new LuckMapFactory();
var fiveLuckFactory = new LuckFactory(luckMapFactory.Make());
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
