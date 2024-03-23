// ------------------------------------
// The Pale Bazaar
// ------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PaleBazaar.MechanistTower.Manipulators;
using PaleBazaar.MechanistTower.PuzzleBoxWorkshop.CipherBox;

namespace PaleBazaar.Pages.Grimoires.PuzzleBoxGames;

public partial class CipherBox : ComponentBase
{
    private IBrowserFile UploadImage;

    public int CustomBoardSize { get; set; }

    public GameRune[,] GameBoard => CipherBoard.GameBoard;

    public int GameBoardSize => CipherBoard.GameBoardSize;

    public int TeleportTranscript => CipherBoard.TeleportTranscript;

    public bool Victory => CipherBoard.Victory;

    [Inject]
    private IEchoShaper _echoShaper { get; set; }

    private CipherBoard CipherBoard { get; set; }

    private bool ShowCounter { get; set; } = true;

    private bool ShowSigil { get; set; } = false;

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

    protected override void OnInitialized()
    {
        CipherBoard = new CipherBoard(4);
    }

    private async Task CustomizeRunes()
    {
        var paths = await _echoShaper.SplitCipherEcho(UploadImage, CustomBoardSize);

        CipherBoard.ConjureBoard(CustomBoardSize, paths);
    }

    private void HandleCustomImage(InputFileChangeEventArgs e)
    {
        UploadImage = e.File;
    }
}