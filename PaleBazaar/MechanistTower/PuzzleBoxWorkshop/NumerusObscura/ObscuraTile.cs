// ------------------------------------
// The Pale Bazaar
// ------------------------------------

namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.NumerusObscura;

public class ObscuraTile
{
    public int Column { get; set; }

    public bool IsMarked { get; set; }

    public int Position { get; set; }
    public int Row { get; set; }

    public TileState State { get; set; }

    public ObscuraTile(int row, int column, int position, TileState state)
    {
        Column = column;
        Position = position;
        Row = row;
        State = state;
    }
}