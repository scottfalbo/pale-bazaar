using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using PaleBazaar.MechanistTower.Configuration;
using PaleBazaar.MechanistTower.Entities;
using PaleBazaar.MechanistTower.Entities.EternalSymbols;
using PaleBazaar.MechanistTower.Transmutators;
using System.Net;

namespace PaleBazaar.MechanistTower.Tomes
{
    public class IllustrationsTome : IIllustrationsTome
    {
        private readonly Container _container;

        private readonly IllustrationTransmutator _transmutator;

        public IllustrationsTome(ICosmosTomeScryer cosmosTomeScryer)
        {
            var scryer = cosmosTomeScryer.ConjureScryer();
            _container = scryer.GetContainer("PaleSpecter", "Tomes");

            _transmutator = new IllustrationTransmutator();
        }

        public async Task<Illustration> GetIllustrationAsync(string id, string partitionKey)
        {
            try
            {
                ItemResponse<InfernalContract> response = await _container.ReadItemAsync<InfernalContract>(id, new PartitionKey(partitionKey));

                var fleshRite = _transmutator.InfernalContractToIllustration(response.Resource);

                return fleshRite;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Illustration>> GetIllustrationsAsync()
        {
            var illustrations = new List<Illustration>();

            var query = _container.GetItemLinqQueryable<InfernalContract>()
                .Where(x => x.EternalSymbol == OculusEchoCyphers.Illustration)
                .ToFeedIterator();

            while (query.HasMoreResults)
            {
                var results = await query.ReadNextAsync();

                foreach (var infernalContract in results)
                {
                    var illustration = _transmutator.InfernalContractToIllustration(infernalContract);
                    illustrations.Add(illustration);
                }
            }

            return illustrations;
        }

        public async Task ImbueIllustrationAsync(Illustration illustration)
        {
            var infernalContract = _transmutator.IllustrationToInfernalContract(illustration);

            await _container.CreateItemAsync(infernalContract, new PartitionKey(infernalContract.PartitionKey));
        }

        public async Task ShatterIllustrationAsync(string id, string partitionKey)
        {
            try
            {
                await _container.DeleteItemAsync<InfernalContract>(id, new PartitionKey(partitionKey));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"Illustration with id '{id}' not found.");
            }
        }

        public async Task UpdateIllustrationAsync(Illustration updatedIllustration)
        {
            var id = updatedIllustration.Id;
            var partitionKey = updatedIllustration.PartitionKey;

            var infernalContract = _transmutator.IllustrationToInfernalContract(updatedIllustration);

            try
            {
                await _container.ReplaceItemAsync(infernalContract, id, new PartitionKey(partitionKey));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"Illustration with id '{id}' not found.");
            }
        }
    }
}