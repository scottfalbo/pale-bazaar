// ------------------------------------
// The Pale Bazaar
// ------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PaleBazaar.Pages;

public partial class Index
{
    private bool ShowBanner { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    private async Task HideBanner()
    {
        ShowBanner = false;
        await JSRuntime.InvokeVoidAsync("sessionStorage.setItem", "ShowBanner", "false");
    }

    protected override void OnInitialized()
    {
        ShowBanner = true;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var result = await JSRuntime.InvokeAsync<string>("sessionStorage.getItem", "ShowBanner");
            if (string.IsNullOrEmpty(result))
            {
                ShowBanner = true;
            }
            else
            {
                ShowBanner = result.ToLower() == "true";
            }

            StateHasChanged();
        }
    }
}