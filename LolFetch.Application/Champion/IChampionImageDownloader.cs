namespace LolFetch.Application.Champion;

using Core;

/**
 * <summary>
 *  IChampionImageDownloader is a service that downloads Champion Images from DataDragon.
 * </summary>
 */
public interface IChampionImageDownloader
{
    /**
     * <summary>
     *  Downloads the champion's splash arts from Data Dragon
     * </summary>
     */
    public Task<byte[]> DownloadLoadingSplashArt(Champion champion);

    /**
     * <summary>
     *  Downloads the champion's square splash arts from Data Dragon.
     * </summary>
     */
    public Task<byte[]> DownloadSquareSplashArt(Champion champion);
}