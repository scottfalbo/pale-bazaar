// ------------------------------------
// The Pale Bazaar
// ------------------------------------

namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.PlagueDoctor;

public class Cell
{
    public int CellColumn { get; set; }
    public int CellRow { get; set; }
    public CellState State { get; set; }

    public Cell(CellState state, int cellColumn, int cellRow)
    {
        State = state;
        CellColumn = cellColumn;
        CellRow = cellRow;
    }
}