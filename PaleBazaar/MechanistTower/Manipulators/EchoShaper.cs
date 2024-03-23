using Microsoft.AspNetCore.Components.Forms;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Text.RegularExpressions;

namespace PaleBazaar.MechanistTower.Manipulators;

public class EchoShaper : IEchoShaper
{
    public string AugmentRunicNaming(string fileName)
    {
        var uniqueId = Guid.NewGuid().ToString();

        var pattern = @"[^.]+$";
        var fileType = Regex.Match(fileName, pattern).ToString();

        fileName = Regex.Replace(fileName, $@"\b.{fileType}\b", "");
        fileName = fileName.Replace(" ", String.Empty);

        var augmentedRune = $"{fileName}-{uniqueId}.{fileType}";

        return augmentedRune;
    }

    public async Task<Stream> ShapeEcho(IBrowserFile file, int height, int maxWidth = int.MaxValue)
    {
        var memoryStream = new MemoryStream();
        await file.OpenReadStream().CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        using var echo = Image.Load(memoryStream);
        var stream = new MemoryStream();

        var newWidth = (int)((double)echo.Width / echo.Height * height);
        echo.Mutate(x => x.Resize(newWidth, height));

        if (newWidth > maxWidth)
        {
            var cropAmount = (newWidth - maxWidth) / 2;
            var cropRectangle = new Rectangle(cropAmount, 0, maxWidth, height);
            echo.Mutate(x => x.Crop(cropRectangle));
        }

        switch (file.ContentType)
        {
            case "image/jpeg":
                echo.SaveAsJpeg(stream);
                break;

            case "image/png":
                echo.SaveAsPng(stream);
                break;

            case "image/bmp":
                echo.SaveAsBmp(stream);
                break;

            case "image/gif":
                echo.SaveAsGif(stream);
                break;

            default:
                throw new Exception("invalid file type");
        }

        stream.Position = 0;
        return stream;
    }

    public async Task<List<string>> SplitCipherEcho(IBrowserFile file, int boardSize)
    {
        var images = new List<Image<Rgba32>>();

        using var memoryStream = new MemoryStream();
        await file.OpenReadStream().CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        using var image = Image.Load<Rgba32>(memoryStream);

        var minDim = Math.Min(image.Width, image.Height);
        var cropRectangle = new Rectangle((image.Width - minDim) / 2, (image.Height - minDim) / 2, minDim, minDim);

        image.Mutate(ctx => ctx.Crop(cropRectangle));

        image.Mutate(ctx => ctx.Resize(new ResizeOptions
        {
            Size = new Size(boardSize * 80, boardSize * 80),
            Mode = ResizeMode.Max
        }));

        var width = image.Width / boardSize;
        var height = image.Height / boardSize;

        for (var y = 0; y < boardSize; y++)
        {
            for (var x = 0; x < boardSize; x++)
            {
                var sourceRect = new Rectangle(x * width, y * height, width, height);
                var clone = image.Clone(ctx => ctx.Crop(sourceRect));
                images.Add(clone);
            }
        }

        return ConvertSplitImage(images);
    }

    private static List<string> ConvertSplitImage(List<Image<Rgba32>> images)
    {
        var imagePaths = new List<string>();

        foreach (var image in images)
        {
            using var ms = new MemoryStream();
            image.Save(ms, new PngEncoder());
            var byteImage = ms.ToArray();
            var path = Convert.ToBase64String(byteImage);

            imagePaths.Add(path);
        }

        return imagePaths;
    }
}