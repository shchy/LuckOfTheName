
public class LuckOfTheNameModule : NancyModule
{
	public LuckOfTheNameModule()
		: base("lotn")
	{
		Get["/"] = _ => 
		{
			

			return "hello";
			//return View["index"]; // "Nancy running on ScriptCS!";
		};
	}
}