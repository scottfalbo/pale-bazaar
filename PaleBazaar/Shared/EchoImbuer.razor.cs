// ------------------------------------
// The Pale Bazaar
// ------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PaleBazaar.MechanistTower.SpellChanters;

namespace PaleBazaar.Shared;

public partial class EchoImbuer : ComponentBase
{
    private string AltText = "Flesh Rite by Scott Falbo";

    private ImbueEchoModel ImbueModel = new ImbueEchoModel();

    private string Name;

    [Inject]
    public IEchoChanters EchoChanters { get; set; }

    [Parameter]
    public string EternalSymbol { get; set; }

    [Parameter]
    public EventCallback ImbueEchoSubmitted { get; set; }

    private void HandleFileSelected(InputFileChangeEventArgs e)
    {
        ImbueModel.UploadedFiles = e.GetMultipleFiles().ToArray();
    }

    private async Task ImbueEcho()
    {
        InscribeDefaults();

        if (ImbueModel.UploadedFiles is not null && ImbueModel.UploadedFiles.Length > 0)
        {
            await EchoChanters.ImbueEcho(ImbueModel.UploadedFiles, EternalSymbol, ImbueModel.Name, ImbueModel.AltText);
            await ImbueEchoSubmitted.InvokeAsync(null);
        }
    }

    private void InscribeDefaults()
    {
        if (string.IsNullOrEmpty(Name))
        {
            Name = $"Untitled {EternalSymbol}";
        }

        if (string.IsNullOrEmpty(AltText))
        {
            AltText = $"{EternalSymbol} by Scott Falbo";
        }
    }

    private class ImbueEchoModel
    {
        public string AltText { get; set; }
        public string Name { get; set; }
        public IBrowserFile[] UploadedFiles { get; set; }
    }
}