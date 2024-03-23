// ------------------------------------
// The Pale Bazaar
// ------------------------------------

namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.NumerusObscura;

public class ObscuraTile
{
    public int Column { get; set; }

    public bool IsMarked { get; set; }

    public int Row { get; set; }

    public TileState State { get; set; }
}