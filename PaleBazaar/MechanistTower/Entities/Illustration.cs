using PaleBazaar.MechanistTower.Entities.EternalSymbols;

namespace PaleBazaar.MechanistTower.Entities
{
    public class Illustration : OculusEcho
    {
        public string Medium { get; set; }
        public string Size { get; set; }
        public bool IsForSale { get; set; }
        public decimal Price { get; set; }

        public Illustration() : base(OculusEchoCyphers.Illustration)
        {
        }
    }
}