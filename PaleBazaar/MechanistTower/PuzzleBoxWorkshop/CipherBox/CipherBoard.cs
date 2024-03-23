// ------------------------------------
// The Pale Bazaar
// ------------------------------------

namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.CipherBox;

public class CipherBoard
{
    public GameRune[,] GameBoard { get; set; }
    public int GameBoardSize { get; set; }
    public GameRune ExiledRune { get; set; }
    public bool Victory => IsVictorious();

    public int TeleportTranscript;

    public CipherBoard(int gameBoardSize)
    {
        ConjureBoard(gameBoardSize);
    }

    public void ConjureBoard(int gameBoardSize, List<string> imagePaths = null)
    {
        GameBoard = new GameRune[gameBoardSize, gameBoardSize];
        GameBoardSize = gameBoardSize;

        ConjureRunes(gameBoardSize, imagePaths);
        ExileRune();
        ScatterRunes();

        TeleportTranscript = 0;
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

    private void ConjureRunes(int gameBoardSize, List<string> imagePaths = null)
    {
        var index = 0;
        var customImage = imagePaths != null;

        for (int x = 0; x < gameBoardSize; x++)
        {
            for (int y = 0; y < gameBoardSize; y++)
            {
                GameBoard[x, y] = new GameRune
                {
                    Sigil = index,
                    X = x,
                    Y = y,
                    SolvedX = x,
                    SolvedY = y,
                    ImageUrl = customImage ? $"data:image/png;base64,{imagePaths[index]}"
                        : $"/images/puzzle-box/cipher-box/default_luci_{index}.png"
                };

                index++;
            }
        }
    }

    private void ScatterRunes()
    {
        var random = new Random();

        for (var i = 0; i < 1000; i++)
        {
            var randomX = random.Next(0, GameBoardSize);
            var randomY = random.Next(0, GameBoardSize);

            var randomX2 = random.Next(0, GameBoardSize);
            var randomY2 = random.Next(0, GameBoardSize);

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

    private void ExileRune()
    {
        var index = GameBoardSize - 1;

        ExiledRune = GameBoard[index, index];
        GameBoard[index, index] = null;
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
            var (currentX, _) when currentX + 1 < GameBoardSize && GameBoard[currentX + 1, y] == null => (currentX + 1, y),
            var (_, currentY) when currentY - 1 >= 0 && GameBoard[x, currentY - 1] == null => (x, currentY - 1),
            var (_, currentY) when currentY + 1 < GameBoardSize && GameBoard[x, currentY + 1] == null => (x, currentY + 1),
            _ => null
        };
    }
}