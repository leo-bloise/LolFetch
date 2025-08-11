namespace LolFetch.Core;

/**
 * <summary>
 *  Canvas pixel data to be rendered in Canvas. It holds a Color and a Symbol.
 * </summary>
 */
public record CanvasPixel(CanvasColor? Color, char Symbol)
{
    public override string ToString()
    {
        return Symbol.ToString();
    }
}