using System.Net.Http.Json;
using System.Text.Json;
using LolFetch.Application;
using LolFetch.Application.DataDragon;

namespace LolFetch.Infrastructure;

public class VersionDownloader : IVersionDownloader, IDisposable
{
    private readonly DdUriBuilder _ddUriBuilder;

    private static readonly string OLD_VERSION_MARKER = "lolpatch";

    private readonly HttpClient _client;

    public VersionDownloader(DdUriBuilder ddUriBuilder, HttpClient client)
    {
        _ddUriBuilder = ddUriBuilder;
        _client = client;
    }
    
    public async Task<IEnumerable<Core.Version>> GetAllVersionsAsync()
    {
        try
        {
            Uri uri = _ddUriBuilder.BuildAllVersionsUri();
            
            using HttpResponseMessage httpResponseMessage = await _client.GetAsync(uri);

            httpResponseMessage.EnsureSuccessStatusCode();

            string[]? allVersions = await httpResponseMessage.Content.ReadFromJsonAsync<string[]>();

            if (allVersions == null) throw new HttpRequestException("all versions response body was empty");

            return allVersions.Where(rawVersion => rawVersion.IndexOf(OLD_VERSION_MARKER) == -1)
                .Select(rawVersion => Core.Version.Parse(rawVersion));
        }
        catch (JsonException exception)
        {
            throw new HttpRequestException(exception.Message, exception);;
        }
    }
    
    public async Task<Core.Version> GetLatestVersion()
    {
        IEnumerable<Core.Version> versions = await GetAllVersionsAsync();

        return versions.First();
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}