using LolFetch.Application.DataDragon;
using LolFetch.Core;

namespace LolFetch.Infrastructure;

using Application.Champion;

public class ChampionImageDownloader : IChampionImageDownloader, IDisposable
{
    private DdUriBuilder _ddUriBuilder;

    private readonly HttpClient _httpClient = new();
    
    public ChampionImageDownloader(DdUriBuilder ddUriBuilder)
    {
        _ddUriBuilder = ddUriBuilder;
    }
    
    public async Task<byte[]> DownloadLoadingSplashArt(Champion champion)
    {
        Uri uri = _ddUriBuilder.BuildChampionsUri(champion);
        
        using HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(uri);
        
        httpResponseMessage.EnsureSuccessStatusCode();

        return await httpResponseMessage.Content.ReadAsByteArrayAsync();
    }

    public async Task<byte[]> DownloadSquareSplashArt(Champion champion)
    {
        Uri uri = _ddUriBuilder.BuildSquareChampionsUri(champion);
        
        using HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(uri);
        
        httpResponseMessage.EnsureSuccessStatusCode();

        return await httpResponseMessage.Content.ReadAsByteArrayAsync();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}