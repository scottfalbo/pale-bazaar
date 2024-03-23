namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.PlagueDoctor;

public class Capsule
{
    public int Column { get; set; }
    public int Row { get; set; }
    public string Color { get; set; }
    public bool IsAttached { get; set; }

    public Capsule(int column, int row, string color)
    {
        Column = column;
        Row = row;
        Color = color;
        IsAttached = true;
    }
}