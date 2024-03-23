// ------------------------------------
// The Pale Bazaar
// ------------------------------------

namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.NumerusObscura;

public class Calculations
{
    public bool MarkTile(decimal percentage)
    {
        var random = new Random();
        var randomValue = random.NextDouble();
        return randomValue <= (double)percentage;
    }

    public decimal RandomPercentage(int low, int high)
    {
        var random = new Random();
        var randomPercentage = random.Next(low, high) / 100m;
        return randomPercentage;
    }
}