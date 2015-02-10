
public class LuckOfTheNameModule : NancyModule
{
	public LuckOfTheNameModule()
		: base("lotn")
	{
		Get["/"] = _ => 
		{
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

		    var w = new StringBuilder();
	        foreach (var item in xs)
	        {
	            w.AppendLine("-----------------------------------");
	            w.AppendLine(item.sum.ToString());
	            w.AppendLine(fiveLuckView.View(item.lucks));
	 
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
	                w.AppendFormat("{0} {1} {2}",test, x00, x01);
	                w.AppendLine();
	            }
	 
	            //names.ForEach(w.WriteLine);
	        }


			var model = new{Text = w.ToString().Replace(Environment.NewLine, "</br>")};
			return View["index", model];
			//return View["index"]; // "Nancy running on ScriptCS!";
		};
	}
}