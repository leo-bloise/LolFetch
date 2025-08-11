using LolFetch.Application.Champion;
using LolFetch.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace LolFetch.Infrastructure;

public class AscIIDraftsman : IAscIIDraftsman
{
    private const char PAINT = '█';

    private Image<Rgba32> ParseImageAndResizeToCanvas(byte[] image, Canvas canvas)
    {
        Image<Rgba32> imageParsed = Image.Load<Rgba32>(image);

        int newWidth = imageParsed.Width > canvas.Width ? canvas.Width : imageParsed.Width;
        int newHeight = imageParsed.Height > canvas.Height ? canvas.Height : imageParsed.Height;
        
        imageParsed.Mutate(x => x.Resize(newWidth, newHeight));
        
        return imageParsed;
    }

    private CanvasColor CreateCanvasColor(Rgba32 pixel, bool realColor)
    {
        if (realColor) return new CanvasColor(pixel.R, pixel.G, pixel.B);
        
        float brightness = (0.2126f * pixel.R + 0.7152f * pixel.G + 0.0722f * pixel.B) / 255f;
        int gray = (byte)(brightness * 255);

        return new CanvasColor(gray, gray, gray);
    }
    
    public void DrawAscii(byte[] image, Canvas canvas, bool useRealColors = false)
    {
        using Image<Rgba32> imageParsed = ParseImageAndResizeToCanvas(image, canvas); 

        for (int y = 0; y < imageParsed.Height; y++)
        {
            for (int x = 0; x < imageParsed.Width; x++)
            {
                Rgba32 pixel = imageParsed[x, y];
                
                canvas.Draw(x, y, PAINT, CreateCanvasColor(pixel, useRealColors));
            }
        }
    }
}