// ------------------------------------
// The Pale Bazaar
// ------------------------------------

using Microsoft.AspNetCore.Components.Forms;
using PaleBazaar.MechanistTower.Entities;
using PaleBazaar.MechanistTower.Tomes;

namespace PaleBazaar.MechanistTower.SpellChanters;

public class EchoChanters : IEchoChanters
{
    private readonly IEchoesTome _echoesTome;
    private readonly IEchoKeeperChanter _echoKeeperChanter;

    public EchoChanters(IEchoesTome echoesTome, IEchoKeeperChanter echoKeeperChanter)
    {
        _echoesTome = echoesTome;
        _echoKeeperChanter = echoKeeperChanter;
    }

    public async Task<List<Echo>> GetEchoes(string eternalSymbol)
    {
        var echoes = await _echoesTome.GetEchoesAsync(eternalSymbol);

        return echoes.ToList();
    }

    public async Task ImbueEcho(IBrowserFile[] files, string eternalSymbol, string name, string altText)
    {
        foreach (var file in files)
        {
            var echo = new Echo(eternalSymbol)
            {
                Name = name,
                AltText = altText,
            };

            await _echoKeeperChanter.InscribeEcho(file, echo);

            await _echoesTome.ImbueEchoAsync(echo);
        }
    }

    public async Task ShatterEcho(string id, string partitionKey, string fileName, string thumbnailFileName)
    {
        await _echoKeeperChanter.BanishEcho(fileName);
        await _echoKeeperChanter.BanishEcho(thumbnailFileName);

        await _echoesTome.ShatterEchoAsync(id, partitionKey);
    }
}