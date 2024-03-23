// ------------------------------------
// The Pale Bazaar
// ------------------------------------

namespace PaleBazaar.MechanistTower.Entities;

public class Echo : GreaterBinding
{
    public string AltText { get; set; }

    public bool Display { get; set; }

    public string FileName { get; set; }

    public string ImageUrl { get; set; }

    public bool IsForSale { get; set; }

    public string Medium { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string Size { get; set; }

    public string ThumbnailFileName { get; set; }

    public string ThumbnailUrl { get; set; }

    public Echo()
    {
    }

    public Echo(string eternalSymbol) : base(eternalSymbol)
    {
    }
}