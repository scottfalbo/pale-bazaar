// ------------------------------------
// The Pale Bazaar
// ------------------------------------

namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.NumerusObscura;

public class ObscuraBoard
{
    public ObscuraTile[,] GameBoard { get; set; }
    private int _markedTileCount { get; set; }

    private int _tileCount { get; set; }
}