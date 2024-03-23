using Microsoft.AspNetCore.Components;
using PaleBazaar.MechanistTower.PuzzleBoxWorkshop.PlagueDoctor;

namespace PaleBazaar.Pages.Grimoires.PuzzleBoxGames
{
    public partial class PlagueDoctor : ComponentBase
    {
        private PlagueJar PlagueJar { get; set; }

        protected override void OnInitialized()
        {
            PlagueJar = new PlagueJar();
        }

        private char RenderCell(int y, int x)
        {
            var cell = PlagueJar.Board[x, y];
            var cellState = cell.State;
            return cellState.ToString()[0];
        }
    }
}