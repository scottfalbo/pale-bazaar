namespace PaleBazaar.MechanistTower.PuzzleBoxWorkshop.CipherBox;

public class GameRune
{
    public int Sigil { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int SolvedX { get; set; }
    public int SolvedY { get; set; }
    public string ImageUrl { get; set; }
    public bool IsCorrect => X == SolvedX && Y == SolvedY;
}