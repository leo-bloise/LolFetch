using System.Text;
using LolFetch.Application.Output;
using LolFetch.Core;

namespace LolFetch.Infrastructure.Output;

public class CanvasOutputTerminal : ICanvasOutputDevice
{
    public void Render(Canvas canvas)
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        for (int y = 0; y < canvas.Height; y++)
        {
            for (int x = 0; x < canvas.Width; x++)
            {
                CanvasPixel? canvasPixel = canvas.GetPixel(x, y);

                if (canvasPixel is null) continue;

                if (canvasPixel.Color == null)
                {
                    stringBuilder.Append(canvasPixel.Symbol);
                    continue;
                }
                
                CanvasColor canvasColor = canvasPixel.Color;
                
                stringBuilder.Append($"\u001b[38;2;{canvasColor.R};{canvasColor.G};{canvasColor.B}m{canvasPixel.Symbol}\u001b[0m");;
            }
            
            stringBuilder.AppendLine();
        }
        
        Console.WriteLine(stringBuilder.ToString());
    }
}