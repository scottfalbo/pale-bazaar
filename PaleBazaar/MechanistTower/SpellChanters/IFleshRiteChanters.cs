using PaleBazaar.MechanistTower.Entities;

namespace PaleBazaar.MechanistTower.SpellChanters
{
    public interface IFleshRiteChanters
    {
        Task<List<Echo>> GetFleshRites();

        Task ImbueEcho(IFormFile[] files, string name, string altText);

        Task ShatterEcho(string id, string partitionKey, string fileName, string thumbnailFileName);
    }
}