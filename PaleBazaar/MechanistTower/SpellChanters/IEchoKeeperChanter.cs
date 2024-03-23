// ------------------------------------
// The Pale Bazaar
// ------------------------------------

using Microsoft.AspNetCore.Components.Forms;
using PaleBazaar.MechanistTower.Entities;

namespace PaleBazaar.MechanistTower.SpellChanters;

public interface IEchoKeeperChanter
{
    public Task BanishEcho(string fileName);

    public Task InscribeEcho(IBrowserFile file, Echo echo);
}