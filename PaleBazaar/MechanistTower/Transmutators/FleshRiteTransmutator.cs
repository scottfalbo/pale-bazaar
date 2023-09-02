using PaleBazaar.MechanistTower.Entities;
using Riok.Mapperly.Abstractions;

namespace PaleBazaar.MechanistTower.Transmutators
{
    [Mapper]
    public partial class FleshRiteTransmutator
    {
        public partial FleshRite InfernalContractToFleshRite(InfernalContract infernalContract);

        public partial InfernalContract FleshRiteToInfernalContract(FleshRite fleshRite);
    }
}