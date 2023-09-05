using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using PaleBazaar.MechanistTower.Configuration;
using PaleBazaar.MechanistTower.Entities;
using PaleBazaar.MechanistTower.Transmutators;
using System.Net;

namespace PaleBazaar.MechanistTower.Tomes
{
    public class EchoesTome : IEchoesTome
    {
        private readonly Container _container;

        private readonly EchoTransmutator _transmutator;

        public EchoesTome(ICosmosTomeScryer cosmosTomeScryer)
        {
            var scryer = cosmosTomeScryer.ConjureScryer();
            _container = scryer.GetContainer("PaleSpecter", "Tomes");

            _transmutator = new EchoTransmutator();
        }

        public async Task ImbueEchoAsync(Echo echo)
        {
            var infernalContract = _transmutator.EchoToInfernalContract(echo);

            await _container.CreateItemAsync(infernalContract, new PartitionKey(infernalContract.PartitionKey));
        }

        public async Task<IEnumerable<Echo>> GetEchoesAsync(string eternalSymbol)
        {
            var echoes = new List<Echo>();

            var query = _container.GetItemLinqQueryable<InfernalContract>()
                .Where(x => x.EternalSymbol == eternalSymbol)
                .ToFeedIterator();

            while (query.HasMoreResults)
            {
                var results = await query.ReadNextAsync();

                foreach (var infernalContract in results)
                {
                    var echo = _transmutator.InfernalContractToEcho(infernalContract);
                    echoes.Add(echo);
                }
            }

            return echoes;
        }

        public async Task<Echo> GetEchoAsync(string id, string partitionKey)
        {
            try
            {
                var response = await _container.ReadItemAsync<InfernalContract>(id, new PartitionKey(partitionKey));

                var echo = _transmutator.InfernalContractToEcho(response.Resource);

                return echo;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task UpdateEchoAsync(Echo updatedEcho)
        {
            var id = updatedEcho.Id;
            var partitionKey = updatedEcho.PartitionKey;

            var infernalContract = _transmutator.EchoToInfernalContract(updatedEcho);

            try
            {
                await _container.ReplaceItemAsync(infernalContract, id, new PartitionKey(partitionKey));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"FleshRite with id '{id}' not found.");
            }
        }

        public async Task ShatterEchoAsync(string id, string partitionKey)
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