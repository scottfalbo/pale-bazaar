using Microsoft.AspNetCore.Components;
using PaleBazaar.MechanistTower.Entities;
using PaleBazaar.MechanistTower.SpellChanters;

namespace PaleBazaar.Pages.Grimoires
{
    public partial class FleshRites
    {
        [Inject]
        private IFleshRiteChanters _fleshRiteChanters { get; set; }
        private List<FleshRite> Echoes { get; set; }
        private int ActiveImageIndex { get; set; } = 0;

        protected override async Task OnInitializedAsync()
        {
            Echoes = await _fleshRiteChanters.GetFleshRites();
        }

        void ShowImage(int index)
        {
            ActiveImageIndex = index;
        }
    }
}
