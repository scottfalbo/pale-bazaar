using PaleBazaar.MechanistTower.Entities;
using Riok.Mapperly.Abstractions;

namespace PaleBazaar.MechanistTower.Transmutators
{
    [Mapper]
    public partial class EchoTransmutator
    {
        public partial Echo InfernalContractToEcho(InfernalContract infernalContract);

        public partial InfernalContract EchoToInfernalContract(Echo echo);
    }
}