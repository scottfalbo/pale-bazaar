// ------------------------------------
// The Pale Bazaar
// ------------------------------------

namespace PaleBazaar.MechanistTower.Entities;

public abstract class GreaterBinding
{
    public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;

    public string EternalSymbol { get; set; }

    public string Id { get; set; }

    public string PartitionKey { get; set; }

    public GreaterBinding(string eternalSymbol)
    {
        EternalSymbol = eternalSymbol;
        Id = Guid.NewGuid().ToString();
        PartitionKey = eternalSymbol;
    }

    public GreaterBinding()
    {
    }
}