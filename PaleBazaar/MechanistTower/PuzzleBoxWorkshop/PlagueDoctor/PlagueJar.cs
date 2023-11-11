namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.PlagueDoctor
{
    public class PlagueJar
    {
        private const int BoardWidth = 10;
        private const int BoardHeight = 20;
        private int[,] Board = new int[BoardWidth, BoardHeight];

        public PlagueJar()
        {
            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    Board[x, y] = 0;
                }
            }
        }
    }
}