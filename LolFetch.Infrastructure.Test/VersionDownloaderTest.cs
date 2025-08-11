using System.Net;
using LolFetch.Application;
using LolFetch.Application.DataDragon;
using LolFetch.Core.DataDragon;
using SoloX.CodeQuality.Test.Helpers.Http;
using Version = LolFetch.Core.Version;

namespace LolFetch.Infrastructure.Test;

public class VersionDownloaderTest
{
    private static Uri BASE_URI = new Uri("https://www.test.com");
    
    private DdUriBuilder _ddUriBuilder;
    
    public VersionDownloaderTest()
    {
        _ddUriBuilder = new DdUriBuilder(new DataDragonMetadata(BASE_URI));
    }
    
    [Fact]
    public async Task GetAllVersionsAsync_Success_ReturnsAllVersions()
    {
        string[] allVersions = new[]
        {
            "1.3.2",
            "1.3.1",
            "1.3.0",
            "1.2.0",
            "1.1.0",      
            "1.0.0"
        };
        
        HttpClient httpClient = new HttpClientMockBuilder()
            .WithBaseAddress(BASE_URI)
            .WithRequest("/api/versions.json", HttpMethod.Get)
            .RespondingJsonContent(allVersions)
            .Build();
            
        IVersionDownloader versionDownloader = new VersionDownloader(_ddUriBuilder, httpClient);

        IEnumerable<Version> versionsResponse = await versionDownloader.GetAllVersionsAsync();
        
        Assert.True(versionsResponse.All(version => allVersions.Contains(version.ToString())));
    }
    
    [Fact]
    public async Task GetAllVersionsAsync_Success_ReturnsAllVersionsIgnoringLolPatch()
    {
        string[] allVersions = new[]
        {
            "1.3.2",
            "1.3.1",
            "1.3.0",
            "1.2.0",
            "1.1.0",      
            "1.0.0",
            "lolpatch_1.0",
            "lolpatch_1.2",
            "lolpatch_1.3"
        };

        IEnumerable<Version> allVersionsExpected = new List<Version>()
        {
            Version.Parse("1.3.2"),
            Version.Parse("1.3.1"),
            Version.Parse("1.3.0"),
            Version.Parse("1.2.0"),
            Version.Parse("1.1.0"),
            Version.Parse("1.0.0")
        };
        
        HttpClient httpClient = new HttpClientMockBuilder()
            .WithBaseAddress(BASE_URI)
            .WithRequest("/api/versions.json", HttpMethod.Get)
            .RespondingJsonContent(allVersions)
            .Build();
            
        IVersionDownloader versionDownloader = new VersionDownloader(_ddUriBuilder, httpClient);

        IEnumerable<Version> versionsResponse = await versionDownloader.GetAllVersionsAsync();
        
        Assert.True(versionsResponse.All(version => allVersionsExpected.Contains(version)));
    }

    [Theory]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.Redirect)]
    public async Task GetAllVersionsAsync_Failure_ThrowsHttpRequestExceptionDueRequestFailure(HttpStatusCode statusCode)
    {
        HttpClient httpClient = new HttpClientMockBuilder()
            .WithBaseAddress(BASE_URI)
            .WithRequest("/api/versions.json", HttpMethod.Get)
            .RespondingStatus(statusCode)
            .Build();
        
        IVersionDownloader versionDownloader = new VersionDownloader(_ddUriBuilder, httpClient);
        
        await Assert.ThrowsAsync<HttpRequestException>(() => versionDownloader.GetAllVersionsAsync());
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task GetAllVersionsAsync_Failure_ThrowsHttpRequestExceptionDueEmptyReturn(string? response)
    {
        HttpClient httpClient = new HttpClientMockBuilder()
            .WithBaseAddress(BASE_URI)
            .WithRequest("/api/versions.json", HttpMethod.Get)
            .RespondingJsonContent(response)
            .Build();
        
        IVersionDownloader versionDownloader = new VersionDownloader(_ddUriBuilder, httpClient);
        
        await Assert.ThrowsAsync<HttpRequestException>(() => versionDownloader.GetAllVersionsAsync());
    }
}