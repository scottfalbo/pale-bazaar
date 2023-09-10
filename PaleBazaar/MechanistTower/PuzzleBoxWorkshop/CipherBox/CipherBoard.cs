namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.CipherBox
{
    public class CipherBoard
    {
        public GameRune[,] GameBoard { get; set; }
        public int GameBoardX { get; set; }
        public int GameBoardY { get; set; }
        public GameRune ExiledRune { get; set; }
        public bool Victory => IsVictorious();

        public int TeleportTranscript;

        public CipherBoard(int x, int y)
        {
            GameBoard = new GameRune[x, y];
            GameBoardX = x;
            GameBoardY = y;

            ConjureRunes();
            ExileRune(x, y);
            ScatterRunes();
        }

        public void TranslocateRune(int x, int y)
        {
            var teleport = ForeseenPath(x, y);

            if (teleport != null)
            {
                var rune = GameBoard[x, y];
                GameBoard[teleport.Value.x, teleport.Value.y] = rune;
                rune.X = teleport.Value.x;
                rune.Y = teleport.Value.y;
                GameBoard[x, y] = null;

                TeleportTranscript++;
            }
        }

        public void ReScatterBoard()
        {
            ScatterRunes();
            TeleportTranscript = 0;
        }

        private void ConjureRunes()
        {
            var counter = 0;

            for (int x = 0; x < GameBoard.GetLength(0); x++)
            {
                for (int y = 0; y < GameBoard.GetLength(1); y++)
                {
                    GameBoard[x, y] = new GameRune
                    {
                        Sigil = counter,
                        X = x,
                        Y = y,
                        SolvedX = x,
                        SolvedY = y,
                        ImageUrl = $"/images/puzzle-box/cipher-box/default_luci_{counter}.png"
                    };

                    counter++;
                }
            }
        }

        private void ScatterRunes()
        {
            var random = new Random();

            for (var i = 0; i < 1000; i++)
            {
                var randomX = random.Next(0, GameBoard.GetLength(0));
                var randomY = random.Next(0, GameBoard.GetLength(1));

                var randomX2 = random.Next(0, GameBoard.GetLength(0));
                var randomY2 = random.Next(0, GameBoard.GetLength(1));

                var runeOne = GameBoard[randomX, randomY];
                if (runeOne != null)
                {
                    runeOne.X = randomX2;
                    runeOne.Y = randomY2;
                }

                var runeTwo = GameBoard[randomX2, randomY2];
                if (runeTwo != null)
                {
                    runeTwo.X = randomX;
                    runeTwo.Y = randomY;
                }

                GameBoard[randomX, randomY] = runeTwo;
                GameBoard[randomX2, randomY2] = runeOne;
            }
        }

        private void ExileRune(int x, int y)
        {
            ExiledRune = GameBoard[x - 1, y - 1];
            GameBoard[x - 1, y - 1] = null;
        }

        private bool IsVictorious()
        {
            foreach (var rune in GameBoard)
            {
                if (rune != null && !rune.IsCorrect)
                {
                    return false;
                }
            }

            return true;
        }

        private (int x, int y)? ForeseenPath(int x, int y)
        {
            return (x, y) switch
            {
                var (currentX, _) when currentX - 1 >= 0 && GameBoard[currentX - 1, y] == null => (currentX - 1, y),
                var (currentX, _) when currentX + 1 < GameBoardX && GameBoard[currentX + 1, y] == null => (currentX + 1, y),
                var (_, currentY) when currentY - 1 >= 0 && GameBoard[x, currentY - 1] == null => (x, currentY - 1),
                var (_, currentY) when currentY + 1 < GameBoardY && GameBoard[x, currentY + 1] == null => (x, currentY + 1),
                _ => null
            };
        }
    }
}