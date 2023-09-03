namespace PaleBazaar.MechanistTower.Entities
{
    public abstract class GreaterBinding
    {
        public string Id { get; set; }
        public string PartitionKey { get; set; }
        public string EternalSymbol { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;

        public GreaterBinding(string eternalSymbol)
        {
            EternalSymbol = eternalSymbol;
            Id = Guid.NewGuid().ToString();
            PartitionKey = eternalSymbol;
        }
    }
}