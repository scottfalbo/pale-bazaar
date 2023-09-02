using PaleBazaar.MechanistTower.Entities;

namespace PaleBazaar.MechanistTower.Tomes
{
    public interface IIllustrationsTome
    {
        Task ImbueIllustrationAsync(Illustration illustration);

        Task<Illustration> GetIllustrationAsync(string id, string partitionKey);

        Task<IEnumerable<Illustration>> GetIllustrationsAsync();

        Task UpdateIllustrationAsync(Illustration updatedIllustration);

        Task ShatterIllustrationAsync(string id, string partitionKey);
    }
}