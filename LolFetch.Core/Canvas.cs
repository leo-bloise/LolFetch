using System.Text;

namespace LolFetch.Core;

/**
 * <summary>
 *  Canvas is the area where the champion will be rendered.
 *  Any resizing calculation taking in consideration the target can use this object to resize the Champion properly.
 * </summary>
 */
public record Canvas(int Height, int Width)
{
    private readonly CanvasPixel?[,] _buffer = new CanvasPixel[Width, Height];

    public CanvasPixel? GetPixel(int x, int y)
    {
        return _buffer[x, y];
    }
    
    public void Draw(int x, int y, char c, CanvasColor? color)
    {
        if (y >= Height || y < 0 || x >= Width || x < 0) throw new IndexOutOfRangeException();

        _buffer[x, y] = new CanvasPixel(color, c);
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                CanvasPixel? pixel =  _buffer[x, y];

                if (pixel is null) continue;
                
                stringBuilder.Append(pixel);
            }
            
            stringBuilder.AppendLine();
        }
        
        return stringBuilder.ToString();
    }
}