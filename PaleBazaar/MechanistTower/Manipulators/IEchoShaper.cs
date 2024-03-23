// ------------------------------------
// The Pale Bazaar
// ------------------------------------

using Microsoft.AspNetCore.Components.Forms;

namespace PaleBazaar.MechanistTower.Manipulators;

public interface IEchoShaper
{
    public string AugmentRunicNaming(string fileName);

    public Task<Stream> ShapeEcho(IBrowserFile file, int height, int maxWidth = int.MaxValue);

    public Task<List<string>> SplitCipherEcho(IBrowserFile file, int boardSize);
}