#load "code/LuckOfTheNameModule.csx"
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


using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.ErrorHandling;
using Nancy.Helpers;
using Nancy.Responses;
using Nancy.TinyIoc;
using Nancy.ViewEngines;

using System.Reflection;

try {

    StaticConfiguration.DisableErrorTraces = false;

    var url = "http://localhost:1234";
	using (var host = new Nancy.Hosting.Self.NancyHost(new Uri(url), new CustomBootstrapper()))
    {
        Console.WriteLine(url);
        host.Start();
        Console.ReadLine();
        host.Stop();
    }

} catch (ReflectionTypeLoadException ex) {
	Console.WriteLine(ex);
    Console.WriteLine(ex.Message);

    ex.LoaderExceptions.ForEach(tlex=>
    	Console.WriteLine(tlex.Message));
       
}

public class CustomBootstrapper : DefaultNancyBootstrapper
{
    protected override void ConfigureConventions(NancyConventions conventions)
    {
        base.ConfigureConventions(conventions);

        conventions.StaticContentsConventions.AddDirectory("css", @"content/css");
        conventions.StaticContentsConventions.AddDirectory("fonts", @"content/fonts");
        conventions.StaticContentsConventions.AddDirectory("js", @"content/js");
    }


    protected override IRootPathProvider RootPathProvider
    {
        get { return new PathProvider(); }
    }
}

public class MyErrorHandler : Nancy.ErrorHandling.IStatusCodeHandler
{
    public void Handle(HttpStatusCode statusCode, NancyContext context)
    {
        context.Response = new GenericFileResponse("View/404.html", "text/html");
    }

    public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
    {
        return statusCode == HttpStatusCode.NotFound;
    }
}

public class PathProvider : IRootPathProvider
{
    public string GetRootPath()
    {
        return Path.Combine("..\\..\\", Environment.CurrentDirectory);
    }
}

