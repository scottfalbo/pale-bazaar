using PaleBazaar.MechanistTower.Entities;
using Riok.Mapperly.Abstractions;

namespace PaleBazaar.MechanistTower.Transmutators
{
    [Mapper]
    public partial class IllustrationTransmutator
    {
        public partial Illustration InfernalContractToIllustration(InfernalContract infernalContract);

        public partial InfernalContract IllustrationToInfernalContract(Illustration illustration);
    }
}