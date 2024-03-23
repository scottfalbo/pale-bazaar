// ------------------------------------
// The Pale Bazaar
// ------------------------------------

using Microsoft.Azure.Cosmos;

namespace PaleBazaar.MechanistTower.Configuration;

public interface ICosmosTomeScryer
{
    CosmosClient ConjureScryer();
}