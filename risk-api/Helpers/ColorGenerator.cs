using System.Drawing;

namespace risk_api.Helpers;

public static class ColorGenerator
{
    private static readonly Random rand = new Random();

    public static string GetRandomColor()
    {
        return "#" + Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256), 255).ToArgb().ToString("X6"); //The "blue" is the alpha. Webrowser encodes it different
    } 
}