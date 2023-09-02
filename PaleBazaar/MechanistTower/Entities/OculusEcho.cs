namespace PaleBazaar.MechanistTower.Entities
{
    public abstract class OculusEcho : GreaterBinding
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string ThumbnailUrl { get; set; }

        public bool Display { get; set; }

        public string AltText { get; set; }

        public string FileName { get; set; }

        public string ThumbnailFileName { get; set; }

        public OculusEcho(string chronicleCode) : base(chronicleCode)
        {
        }
    }
}