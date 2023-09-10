using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PaleBazaar.MechanistTower.PuzzleBoxWorkshop.CipherBox;

namespace PaleBazaar.Pages.Grimoires.PuzzleBoxGames
{
    public partial class CipherBox : ComponentBase
    {
        private CipherBoard CipherBoard { get; set; }

        public int TeleportTranscript => CipherBoard.TeleportTranscript;
        public GameRune[,] GameBoard => CipherBoard.GameBoard;
        public int GameBoardX => CipherBoard.GameBoardX;
        public int GameBoardY => CipherBoard.GameBoardY;
        public bool Victory => CipherBoard.Victory;

        private bool ShowCounter = true;
        private IBrowserFile UploadImage;
        private int BoardSize { get; set; } = 4;

        protected override void OnInitialized()
        {
            CipherBoard = new CipherBoard(4, 4);
        }

        public void ActivateRune(int x, int y)
        {
            if (!Victory)
            {
                CipherBoard.TranslocateRune(x, y);

                if (Victory)
                {
                    CipherBoard.GameBoard[GameBoardX - 1, GameBoardY - 1] = CipherBoard.ExiledRune;
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

        private void CustomizeRunes()
        {
            Console.WriteLine(UploadImage);
            Console.WriteLine(BoardSize);
        }
    }
}