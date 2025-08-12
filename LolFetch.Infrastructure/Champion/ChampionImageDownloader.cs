using System.Net;
using LolFetch.Application.DataDragon;
using LolFetch.Core;
using LolFetch.Core.Exceptions;

namespace LolFetch.Infrastructure;

using Application.Champion;

public class ChampionImageDownloader : IChampionImageDownloader, IDisposable
{
    private DdUriBuilder _ddUriBuilder;

    private readonly HttpClient _httpClient;
    
    public ChampionImageDownloader(DdUriBuilder ddUriBuilder, HttpClient httpClient)
    {
        _ddUriBuilder = ddUriBuilder;
        _httpClient = httpClient;
    }
    
    public async Task<byte[]> DownloadLoadingSplashArt(Champion champion)
    {
        Uri uri = _ddUriBuilder.BuildChampionsUri(champion);
        
        using HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(uri);

        if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
        {
            throw new ChampionNotFound(champion);
        }
        
        httpResponseMessage.EnsureSuccessStatusCode();

        return await httpResponseMessage.Content.ReadAsByteArrayAsync();
    }

    public async Task<byte[]> DownloadSquareSplashArt(Champion champion)
    {
        Uri uri = _ddUriBuilder.BuildSquareChampionsUri(champion);
        
        using HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(uri);
        
        if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
        {
            throw new ChampionNotFound(champion);
        }
        
        httpResponseMessage.EnsureSuccessStatusCode();

        return await httpResponseMessage.Content.ReadAsByteArrayAsync();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}