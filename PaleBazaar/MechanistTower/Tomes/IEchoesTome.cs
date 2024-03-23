// ------------------------------------
// The Pale Bazaar
// ------------------------------------

using PaleBazaar.MechanistTower.Entities;

namespace PaleBazaar.MechanistTower.Tomes;

public interface IEchoesTome
{
    Task<Echo> GetEchoAsync(string id, string partitionKey);

    Task<IEnumerable<Echo>> GetEchoesAsync(string eternalSymbol);

    Task ImbueEchoAsync(Echo fleshRite);

    Task ShatterEchoAsync(string id, string partitionKey);

    Task UpdateEchoAsync(Echo updatedFleshRite);
}