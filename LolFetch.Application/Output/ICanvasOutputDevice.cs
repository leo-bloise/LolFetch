using LolFetch.Core;

namespace LolFetch.Application.Output;

/**
 * <summary>
 *  Output Device for the Canvas. It's responsible for extracting data from the canvas.
 * </summary>
 */
public interface ICanvasOutputDevice
{
    public void Render(Canvas canvas);
}