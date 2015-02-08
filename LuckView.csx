#load "ILuckView.csx"

public class LuckView : ILuckView
{
    public string View(ILuckResult<Luck> fiveLuck)
    {
        var b = new StringBuilder();
        b.AppendFormat("{0}:{1}", "天格", ToView(fiveLuck.Ten));
        b.AppendLine();
        b.AppendFormat("{0}:{1}", "人格", ToView(fiveLuck.Jin));
        b.AppendLine();
        b.AppendFormat("{0}:{1}", "地格", ToView(fiveLuck.Ti));
        b.AppendLine();
        b.AppendFormat("{0}:{1}", "外格", ToView(fiveLuck.Gai));
        b.AppendLine();
        b.AppendFormat("{0}:{1}", "総格", ToView(fiveLuck.Sou));
        b.AppendLine();
        b.AppendFormat("{0}:{1}", "家庭運", ToView(fiveLuck.Katei));
        b.AppendLine();
        b.AppendFormat("{0}:{1}", "社会運", ToView(fiveLuck.Syakai));
        b.AppendLine();
        b.AppendFormat("{0}:{1}", "内運01", ToView(fiveLuck.Nai01));
        b.AppendLine();
        b.AppendFormat("{0}:{1}", "内運02", ToView(fiveLuck.Nai02));
        b.AppendLine();
        return b.ToString();
    }

    private string ToView(Luck luck)
    {
        return
            luck.ToGuards()
            .When(Luck.Bad, "凶")
            .When(Luck.Good, "吉")
            .When(Luck.Better, "大吉")
            .When(Luck.Best, "最大吉")
            .Return("");
    }
}

