namespace LolFetch.Application.Champion;

public class RenderChampionUseCase
{
    private readonly IChampionImageDownloader _championImageDownloader;
    
    private readonly IAscIIDraftsman _asciiDraftsman;
    
    public RenderChampionUseCase(IChampionImageDownloader championImageDownloader, IAscIIDraftsman asciiDraftsman)
    {
        _championImageDownloader = championImageDownloader;
        _asciiDraftsman = asciiDraftsman;
    }

    private Task<byte[]> DownloadChampionImage(RenderChampionRequest request)
    {
        return request.Square ? _championImageDownloader.DownloadSquareSplashArt(request.Champion) : _championImageDownloader.DownloadLoadingSplashArt(request.Champion);
    }
    
    public async Task RenderChampionAsync(RenderChampionRequest renderChampionRequest)
    {
        byte[] championImage = await DownloadChampionImage(renderChampionRequest);

        _asciiDraftsman.DrawAscii(championImage, renderChampionRequest.Canvas, renderChampionRequest.Color);
    }
}