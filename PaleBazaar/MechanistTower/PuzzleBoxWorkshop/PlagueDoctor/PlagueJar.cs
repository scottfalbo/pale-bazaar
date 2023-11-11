namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.PlagueDoctor
{
    public class PlagueJar
    {
        private const int BoardWidth = 8;
        private const int BoardHeight = 16;
        private CellState[,] Board = new CellState[BoardWidth, BoardHeight];

        public PlagueJar()
        {
            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    Board[x, y] = CellState.Empty;
                }
            }
        }
    }
}