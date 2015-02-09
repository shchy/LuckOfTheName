#load "ILuckMapFactory.csx"

public class LuckMapFactory : ILuckMapFactory
{
    public IDictionary<int, Luck> Make()
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
}

