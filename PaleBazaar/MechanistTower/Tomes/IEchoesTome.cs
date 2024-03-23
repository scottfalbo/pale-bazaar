using PaleBazaar.MechanistTower.Entities;

namespace PaleBazaar.MechanistTower.Tomes;

public interface IEchoesTome
{
    Task ImbueEchoAsync(Echo fleshRite);

    Task<Echo> GetEchoAsync(string id, string partitionKey);

    Task<IEnumerable<Echo>> GetEchoesAsync(string eternalSymbol);

    Task UpdateEchoAsync(Echo updatedFleshRite);

    Task ShatterEchoAsync(string id, string partitionKey);
}