#load "ILuckResult.csx"

public interface ILuckView
{
    string View(ILuckResult<Luck> fiveLuck);
}

