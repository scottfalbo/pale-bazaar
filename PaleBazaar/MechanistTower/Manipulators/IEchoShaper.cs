using Microsoft.AspNetCore.Components.Forms;

namespace PaleBazaar.MechanistTower.Manipulators
{
    public interface IEchoShaper
    {
        public Task<Stream> ShapeEcho(IBrowserFile file, int height, int maxWidth = int.MaxValue);

        public string AugmentRunicNaming(string fileName);
    }
}