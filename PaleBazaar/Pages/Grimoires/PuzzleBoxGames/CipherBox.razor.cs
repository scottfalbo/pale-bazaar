using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PaleBazaar.MechanistTower.Manipulators;
using PaleBazaar.MechanistTower.PuzzleBoxWorkshop.CipherBox;

namespace PaleBazaar.Pages.Grimoires.PuzzleBoxGames;

public partial class CipherBox : ComponentBase
{
    [Inject]
    private IEchoShaper _echoShaper { get; set; }

    private CipherBoard CipherBoard { get; set; }

    public int TeleportTranscript => CipherBoard.TeleportTranscript;
    public GameRune[,] GameBoard => CipherBoard.GameBoard;
    public int GameBoardSize => CipherBoard.GameBoardSize;
    public int CustomBoardSize { get; set; }
    public bool Victory => CipherBoard.Victory;

    private bool ShowSigil { get; set; } = false;
    private bool ShowCounter { get; set; } = true;

    private IBrowserFile UploadImage;

    protected override void OnInitialized()
    {
        CipherBoard = new CipherBoard(4);
    }

    public void ActivateRune(int x, int y)
    {
        if (!Victory)
        {
            CipherBoard.TranslocateRune(x, y);

            if (Victory)
            {
                CipherBoard.GameBoard[GameBoardSize - 1, GameBoardSize - 1] = CipherBoard.ExiledRune;
                ShowSigil = false;
            }
        }
    }

    public void ReScatterRunes()
    {
        CipherBoard.ReScatterBoard();
    }

    private void HandleCustomImage(InputFileChangeEventArgs e)
    {
        UploadImage = e.File;
    }

    private async Task CustomizeRunes()
    {
        var paths = await _echoShaper.SplitCipherEcho(UploadImage, CustomBoardSize);

        CipherBoard.ConjureBoard(CustomBoardSize, paths);
    }
}