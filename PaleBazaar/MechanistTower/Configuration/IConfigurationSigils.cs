namespace PaleBazaar.MechanistTower.Configuration
{
    public interface IConfigurationSigils
    {
        public string AdminUserName { get; set; }

        public string AdminPassword { get; set; }

        public string CosmosEndpoint { get; set; }

        public string CosmosKey { get; set; }

        public string BlobConnectionString { get; set; }

        public string BlobContainerName { get; set; }

        public string KeyVaultUri { get; set; }
    }
}