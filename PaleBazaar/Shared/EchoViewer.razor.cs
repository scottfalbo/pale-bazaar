using Microsoft.AspNetCore.Components;
using PaleBazaar.MechanistTower.Entities;
using PaleBazaar.MechanistTower.SpellChanters;

namespace PaleBazaar.Shared
{
    public partial class EchoViewer : ComponentBase
    {
        [Inject]
        public IEchoChanters EchoChanters { get; set; }

        [Parameter]
        public List<Echo> Echoes { get; set; }

        [Parameter]
        public int ActiveEchoIndex { get; set; }

        [Parameter]
        public EventCallback<int> OnEchoEvocation { get; set; }

        [Parameter]
        public EventCallback EchoShattered { get; set; }

        [Parameter]
        public bool IsWizardOverLord { get; set; }

        private Echo EchoToShatter;
        private bool IsCarouselVisible = false;
        private ShatterEchoModal ShatterEchoModalRef;

        private string GetCarouselClasses()
        {
            return IsCarouselVisible ? "" : "hide-me";
        }

        private void ShowCarousel(int index)
        {
            ActiveEchoIndex = index;
            IsCarouselVisible = true;
        }

        private void HideCarousel()
        {
            IsCarouselVisible = false;
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

        private void OpenShatterModal(Echo echo)
        {
            EchoToShatter = echo;
            ShatterEchoModalRef.Show();
        }
    }
}