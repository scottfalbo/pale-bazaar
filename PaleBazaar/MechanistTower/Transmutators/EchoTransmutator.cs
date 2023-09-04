using PaleBazaar.MechanistTower.Entities;
using Riok.Mapperly.Abstractions;

namespace PaleBazaar.MechanistTower.Transmutators
{
    [Mapper]
    public partial class EchoTransmutator
    {
        public Echo InfernalContractToEcho(InfernalContract infernalContract)
        {
            return new Echo(infernalContract.EternalSymbol)
            {
                Name = infernalContract.Name,
                ImageUrl = infernalContract.ImageUrl,
                ThumbnailUrl = infernalContract.ThumbnailUrl,
                Display = infernalContract.Display,
                AltText = infernalContract.AltText,
                FileName = infernalContract.FileName,
                ThumbnailFileName = infernalContract.ThumbnailFileName,
                Medium = infernalContract.Medium,
                Size = infernalContract.Size,
                IsForSale = infernalContract.IsForSale,
                Price = infernalContract.Price
            };
        }

        public partial InfernalContract EchoToInfernalContract(Echo echo);
    }
}