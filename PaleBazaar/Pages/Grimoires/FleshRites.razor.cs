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

        protected override async Task OnInitializedAsync()
        {
            Echoes = await _fleshRiteChanters.GetFleshRites();
        }
    }
}
