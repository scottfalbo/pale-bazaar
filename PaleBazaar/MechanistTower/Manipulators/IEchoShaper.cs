namespace PaleBazaar.MechanistTower.Manipulators
{
    public interface IEchoShaper
    {
        public Stream ShapeEcho(IFormFile file, int height, int maxWidth = int.MaxValue);

        public string AugmentRunicNaming(string fileName);
    }
}