// ------------------------------------
// The Pale Bazaar
// ------------------------------------

namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.PlagueDoctor;

public class PlagueJar
{
    public Cell[,] Board;
    public int BoardHeight { get; set; }
    public int BoardWidth { get; set; }

    public PlagueJar()
    {
        BoardWidth = 8;
        BoardHeight = 16;
        Board = new Cell[BoardWidth, BoardHeight];

        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                Board[x, y] = new Cell(CellState.Empty, cellColumn: x, cellRow: y);
            }
        }
    }
}