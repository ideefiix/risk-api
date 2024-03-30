using System.Drawing;

namespace risk_api.Helpers;

public static class ColorGenerator
{
    private static readonly Random rand = new Random();

    public static string GetRandomColour()
    {
        return Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)).ToArgb().ToString("X6");
    } 
}