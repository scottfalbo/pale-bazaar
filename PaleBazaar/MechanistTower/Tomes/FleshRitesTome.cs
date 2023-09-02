using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using PaleBazaar.MechanistTower.Configuration;
using PaleBazaar.MechanistTower.Entities;
using PaleBazaar.MechanistTower.Entities.EternalSymbols;
using PaleBazaar.MechanistTower.Transmutators;
using System.Net;

namespace PaleBazaar.MechanistTower.Tomes
{
    public class FleshRitesTome : IFleshRitesTome
    {
        private readonly Container _container;

        private readonly FleshRiteTransmutator _transmutator;

        public FleshRitesTome(ICosmosTomeScryer cosmosTomeScryer)
        {
            var scryer = cosmosTomeScryer.ConjureScryer();
            _container = scryer.GetContainer("PaleSpecter", "Tomes");

            _transmutator = new FleshRiteTransmutator();
        }

        public async Task ImbueFleshRiteAsync(FleshRite fleshRite)
        {
            var infernalContract = _transmutator.FleshRiteToInfernalContract(fleshRite);

            await _container.CreateItemAsync(infernalContract, new PartitionKey(infernalContract.PartitionKey));
        }

        public async Task<IEnumerable<FleshRite>> GetFleshRitesAsync()
        {
            var fleshRites = new List<FleshRite>();

            var query = _container.GetItemLinqQueryable<InfernalContract>()
                .Where(x => x.EternalSymbol == OculusEchoCyphers.FleshRite)
                .ToFeedIterator();

            while (query.HasMoreResults)
            {
                var results = await query.ReadNextAsync();

                foreach (var infernalContract in results)
                {
                    var fleshRite = _transmutator.InfernalContractToFleshRite(infernalContract);
                    fleshRites.Add(fleshRite);
                }
            }

            return fleshRites;
        }

        public async Task<FleshRite> GetFleshRiteAsync(string id, string partitionKey)
        {
            try
            {
                ItemResponse<InfernalContract> response = await _container.ReadItemAsync<InfernalContract>(id, new PartitionKey(partitionKey));

                var fleshRite = _transmutator.InfernalContractToFleshRite(response.Resource);

                return fleshRite;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task UpdateFleshRiteAsync(FleshRite updatedFleshRite)
        {
            var id = updatedFleshRite.Id;
            var partitionKey = updatedFleshRite.PartitionKey;

            var infernalContract = _transmutator.FleshRiteToInfernalContract(updatedFleshRite);

            try
            {
                await _container.ReplaceItemAsync(infernalContract, id, new PartitionKey(partitionKey));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"FleshRite with id '{id}' not found.");
            }
        }

        public async Task ShatterFleshRiteAsync(string id, string partitionKey)
        {
            try
            {
                await _container.DeleteItemAsync<InfernalContract>(id, new PartitionKey(partitionKey));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"FleshRite with id '{id}' not found.");
            }
        }
    }
}