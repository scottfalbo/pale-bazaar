// ------------------------------------
// The Pale Bazaar
// ------------------------------------

using Microsoft.AspNetCore.Components;
using PaleBazaar.MechanistTower.Entities;
using PaleBazaar.MechanistTower.SpellChanters;

namespace PaleBazaar.Shared;

public partial class EchoViewer : ComponentBase
{
    private Echo EchoToShatter;

    private bool IsCarouselVisible = false;

    private ShatterEchoModal ShatterEchoModalRef;

    [Parameter]
    public int ActiveEchoIndex { get; set; }

    [Inject]
    public IEchoChanters EchoChanters { get; set; }

    [Parameter]
    public List<Echo> Echoes { get; set; }

    [Parameter]
    public EventCallback EchoShattered { get; set; }

    [Parameter]
    public bool IsWizardOverLord { get; set; }

    [Parameter]
    public EventCallback<int> OnEchoEvocation { get; set; }

    private string GetCarouselClasses()
    {
        return IsCarouselVisible ? "" : "hide-me";
    }

    private void HideCarousel()
    {
        IsCarouselVisible = false;
    }

    private void OpenShatterModal(Echo echo)
    {
        EchoToShatter = echo;
        ShatterEchoModalRef.Show();
    }

    private async Task ShatterEcho()
    {
        await EchoChanters.ShatterEcho(
            EchoToShatter.Id,
            EchoToShatter.PartitionKey,
            EchoToShatter.FileName,
            EchoToShatter.ThumbnailFileName);

        ShatterEchoModalRef.Hide();
        await EchoShattered.InvokeAsync(null);
    }

    private void ShowCarousel(int index)
    {
        ActiveEchoIndex = index;
        IsCarouselVisible = true;
    }
}