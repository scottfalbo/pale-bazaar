// ------------------------------------
// The Pale Bazaar
// ------------------------------------

using Microsoft.AspNetCore.Components;
using PaleBazaar.MechanistTower.PuzzleBoxWorkshop.NumerusObscura;

namespace PaleBazaar.Pages.Grimoires.PuzzleBoxGames;

public partial class NumerusObscura : ComponentBase
{
    public ObscuraBoard ObscuraBoard { get; set; }

    protected override void OnInitialized()
    {
        ObscuraBoard = ObscuraBoard.Conjure(10, 10);
    }
}