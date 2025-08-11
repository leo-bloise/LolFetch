namespace LolFetch.Core;

/**
 * <summary>
 *  Pixel's color of Canvas. It supports only RGB colors from 0 to 255.
 * </summary>
 */
public record CanvasColor
{
    public int R { get; init; }
    
    public int G { get; init; }
    
    public int B { get; init; }

    public CanvasColor(int r, int g, int b)
    {
        ValidateColor(r);
        ValidateColor(g);
        ValidateColor(b);
        
        R = r;
        G = g;
        B = b;
    }

    private void ValidateColor(int quantity)
    {
        if (quantity < 0 || quantity > 255)
        {
            throw new Exception($"Invalid color quantity of {quantity}. It must be a value between 0 and 255");
        }
    }
}