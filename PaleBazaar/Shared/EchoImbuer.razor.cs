using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PaleBazaar.MechanistTower.SpellChanters;

namespace PaleBazaar.Shared
{
    public partial class EchoImbuer : ComponentBase
    {
        [Inject]
        public IEchoChanters EchoChanters { get; set; }

        private IFormFile[] UploadedFiles;
        private string Name;
        private string AltText = "Flesh Rite by Scott Falbo";
        private ImbueEchoModel ImbueModel = new ImbueEchoModel();

        [Parameter]
        public EventCallback ImbueEchoSubmitted { get; set; }

        [Parameter]
        public string EternalSymbol { get; set; }

        private class ImbueEchoModel
        {
            public IBrowserFile[] UploadedFiles { get; set; }
            public string Name { get; set; }
            public string AltText { get; set; }
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

        private void HandleFileSelected(InputFileChangeEventArgs e)
        {
            ImbueModel.UploadedFiles = e.GetMultipleFiles().ToArray();
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
    }
}