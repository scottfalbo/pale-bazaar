namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.PlagueDoctor;

public class PlagueJar
{
    public int BoardWidth { get; set; }
    public int BoardHeight { get; set; }

    public Cell[,] Board;

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