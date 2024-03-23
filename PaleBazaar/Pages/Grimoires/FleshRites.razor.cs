// ------------------------------------
// The Pale Bazaar
// ------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using PaleBazaar.MechanistTower.Entities;
using PaleBazaar.MechanistTower.Entities.EternalSymbols;
using PaleBazaar.MechanistTower.SpellChanters;

namespace PaleBazaar.Pages.Grimoires;

public partial class FleshRites
{
    [Inject]
    private AuthenticationStateProvider _authenticationStateProvider { get; set; }

    [Inject]
    private IEchoChanters _echoChanters { get; set; }

    [Inject]
    private UserManager<IdentityUser> _userManager { get; set; }

    private int ActiveImageIndex { get; set; } = 0;
    private List<Echo> Echoes { get; set; }
    private bool IsWizardOverlord { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Echoes = await _echoChanters.GetEchoes(OculusEchoCyphers.FleshRite);

        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity.IsAuthenticated)
        {
            var identityUser = await _userManager.GetUserAsync(user);
            IsWizardOverlord = await _userManager.IsInRoleAsync(identityUser, "WizardOverlord");
        }
    }

    private async Task EchoSubmitted()
    {
        Echoes = await _echoChanters.GetEchoes(OculusEchoCyphers.FleshRite);
    }

    private void ShowImage(int index)
    {
        ActiveImageIndex = index;
    }
}