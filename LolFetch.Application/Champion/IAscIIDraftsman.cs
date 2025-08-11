using LolFetch.Core;

namespace LolFetch.Application.Champion;

/**
 * <summary>
 *  Interface responsible for handling with image and image drawing to the Canvas.
 * </summary>
 */
public interface IAscIIDraftsman
{
    /**
     * <summary>
     * Transform the image to ASCII and draw it on the canvas.
     * The image will be resized to fit inside the Canvas.
     * </summary>
     * <params>
     *  <param name="image">image to be drawn</param>
     *  <param name="canvas">canvas to recieve the image</param>
     *  <param name="useRealColors">Determine if the canvas pixels will use real image pixels (following the original image pixel's image) or not</param>
     * </params>
     */
    public void DrawAscii(byte[] image, Canvas canvas, bool useRealColors = false);
}