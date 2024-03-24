// ------------------------------------
// The Pale Bazaar
// ------------------------------------

namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.NumerusObscura;

public class Combulator
{
    public static List<int> DetermineSequence(List<ObscuraTile> tiles)
    {
        var sequence = new List<int>();
        var position = 1;

        var orderedTiles = tiles.OrderBy(x => x.Position).ToList();

        foreach (var tile in orderedTiles)
        {
            var counter = 0;

            if (tile.State == TileState.HasTile)
            {
                counter++;
                if (position == orderedTiles.Count)
                {
                    sequence.Add(counter);
                }
            }
            else
            {
                sequence.Add(counter);
                counter = 0;
            }

            position++;
        }

        return sequence;
    }

    public static bool MarkTile(decimal percentage)
    {
        var random = new Random();
        var randomValue = random.NextDouble();
        return randomValue <= (double)percentage;
    }

    public static decimal RandomPercentage(int low, int high)
    {
        var random = new Random();
        var randomPercentage = random.Next(low, high) / 100m;
        return randomPercentage;
    }
}