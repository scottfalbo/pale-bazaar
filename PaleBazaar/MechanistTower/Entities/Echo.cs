namespace PaleBazaar.MechanistTower.Entities;

public class Echo : GreaterBinding
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string ThumbnailUrl { get; set; }
    public bool Display { get; set; }
    public string AltText { get; set; }
    public string FileName { get; set; }
    public string ThumbnailFileName { get; set; }
    public string Medium { get; set; }
    public string Size { get; set; }
    public bool IsForSale { get; set; }
    public decimal Price { get; set; }

    public Echo()
    {
    }

    public Echo(string eternalSymbol) : base(eternalSymbol)
    {
    }
}