using Microsoft.AspNetCore.Components.Forms;
using PaleBazaar.MechanistTower.Entities;

namespace PaleBazaar.MechanistTower.SpellChanters
{
    public interface IEchoChanters
    {
        Task<List<Echo>> GetEchoes(string eternalSymbol);

        Task ImbueEcho(IBrowserFile[] files, string name, string altText);

        Task ShatterEcho(string id, string partitionKey, string fileName, string thumbnailFileName);
    }
}