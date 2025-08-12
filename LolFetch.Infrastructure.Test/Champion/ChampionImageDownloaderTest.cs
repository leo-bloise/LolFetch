using System.Net;
using System.Security.Cryptography;
using LolFetch.Application.Champion;
using LolFetch.Application.DataDragon;
using LolFetch.Core.DataDragon;
using LolFetch.Core.Exceptions;
using SoloX.CodeQuality.Test.Helpers.Http;
using Version = LolFetch.Core.Version;

namespace LolFetch.Infrastructure.Test.Champion;

public class ChampionImageDownloaderTest
{
    private IChampionImageDownloader _championImageDownloader;
    
    private static Uri BASE_URI = new Uri("https://www.test.com");

    private readonly DdUriBuilder _ddUriBuilder = new(new DataDragonMetadata(BASE_URI));

    private HttpClient CreateMockedSuccessHttpClient(Uri uri)
    {
        return new HttpClientMockBuilder()
            .WithBaseAddress(BASE_URI)
            .WithRequest(uri.AbsolutePath, HttpMethod.Get)
            .Responding(_ =>
            {
                byte[] bytes = new byte[256];
                RandomNumberGenerator.Fill(bytes);
                HttpResponseMessage response = new HttpResponseMessage();
                
                response.Content = new ByteArrayContent(bytes);
                
                return response;
            })
            .Build();
    }

    private HttpClient CreateMockedFailureHttpClient(Uri uri, HttpStatusCode statusCode)
    {
        return new HttpClientMockBuilder()
            .WithBaseAddress(BASE_URI)
            .WithRequest(uri.AbsolutePath, HttpMethod.Get)
            .RespondingStatus(statusCode)
            .Build();
    }
    
    [Fact]
    public async Task DownloadLoadingSplashArtAsync_Success_ReturnsImageBytes()
    {
        Core.Champion champion = new Core.Champion("Aatrox", Version.Parse("0.0.1"));
        Uri championsUri = _ddUriBuilder.BuildChampionsUri(champion);
        
        _championImageDownloader = new ChampionImageDownloader(_ddUriBuilder, CreateMockedSuccessHttpClient(championsUri));
        
        byte[] imageResponse = await _championImageDownloader.DownloadLoadingSplashArt(champion);
        
        Assert.NotNull(imageResponse);
        Assert.NotEmpty(imageResponse);
    }
    
    [Fact]
    public async Task DownloadLoadingSplashArtAsync_ChampionNotFound_ThrowChampionNotFound()
    {
        Core.Champion champion = new Core.Champion("Aatrox", Version.Parse("0.0.1"));
        Uri championsUri = _ddUriBuilder.BuildChampionsUri(champion);
        
        _championImageDownloader = new ChampionImageDownloader(_ddUriBuilder, CreateMockedFailureHttpClient(championsUri, HttpStatusCode.Forbidden));
        
        await Assert.ThrowsAsync<ChampionNotFound>(() => _championImageDownloader.DownloadLoadingSplashArt(champion));
    }
    
    [Theory]
    [InlineData(HttpStatusCode.Unauthorized)]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.Redirect)]
    public async Task DownloadLoadingSplashArtAsync_RequestError_ThrowHttpRequestException(HttpStatusCode statusCode)
    {
        Core.Champion champion = new Core.Champion("Aatrox", Version.Parse("0.0.1"));
        Uri championsUri = _ddUriBuilder.BuildChampionsUri(champion);
        
        _championImageDownloader = new ChampionImageDownloader(_ddUriBuilder, CreateMockedFailureHttpClient(championsUri, statusCode));
        
        await Assert.ThrowsAsync<HttpRequestException>(() => _championImageDownloader.DownloadLoadingSplashArt(champion));
    }

    [Fact]
    public async Task DownloadSquareSplashArtAsync_Success_ReturnsImageBytes()
    {
        Core.Champion champion = new Core.Champion("Aatrox", Version.Parse("0.0.1"));
        Uri championsUri = _ddUriBuilder.BuildSquareChampionsUri(champion);

        _championImageDownloader = new ChampionImageDownloader(_ddUriBuilder, CreateMockedSuccessHttpClient(championsUri));
        
        byte[] imageResponse = await _championImageDownloader.DownloadSquareSplashArt(champion);
        
        Assert.NotNull(imageResponse);
        Assert.NotEmpty(imageResponse);
    }

    [Fact]
    public async Task DownloadSquareSplashArtAsync_ChampionNotFound_ThrowChampionNotFound()
    {
        Core.Champion champion = new Core.Champion("Aatrox", Version.Parse("0.0.1"));
        Uri championsUri = _ddUriBuilder.BuildSquareChampionsUri(champion);
        
        _championImageDownloader = new ChampionImageDownloader(_ddUriBuilder, CreateMockedFailureHttpClient(championsUri, HttpStatusCode.Forbidden));
        
        await Assert.ThrowsAsync<ChampionNotFound>(() => _championImageDownloader.DownloadSquareSplashArt(champion));
    }
    
    [Theory]
    [InlineData(HttpStatusCode.Unauthorized)]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.Redirect)]
    public async Task DownloadSquareSplashArtAsync_RequestError_ThrowHttpRequestException(HttpStatusCode statusCode)
    {
        Core.Champion champion = new Core.Champion("Aatrox", Version.Parse("0.0.1"));
        Uri championsUri = _ddUriBuilder.BuildSquareChampionsUri(champion);
        
        _championImageDownloader = new ChampionImageDownloader(_ddUriBuilder, CreateMockedFailureHttpClient(championsUri, statusCode));
        
        await Assert.ThrowsAsync<HttpRequestException>(() => _championImageDownloader.DownloadSquareSplashArt(champion));
    }
}