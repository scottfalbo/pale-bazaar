namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.PlagueDoctor
{
    public class PlagueJar
    {
        private const int BoardWidth = 8;
        private const int BoardHeight = 16;

        private Cell[,] Board = new Cell[BoardWidth, BoardHeight];

        public PlagueJar()
        {
            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    Board[x, y] = new Cell(CellState.Empty, cellColumn: x, cellRow: y);
                }
            }
        }
    }
}