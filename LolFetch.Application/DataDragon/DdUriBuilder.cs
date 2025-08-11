using LolFetch.Core.DataDragon;

namespace LolFetch.Application.DataDragon;

/**
 * <summary>
 * DataDragon has a bunch of APIs urls that our application is interested in. This builder is responsible for building these URLs easily.
 * </summary>
 */
public class DdUriBuilder
{
    private readonly DataDragonMetadata _dataDragonMetadata;

    public DdUriBuilder(DataDragonMetadata dataDragonMetadata)
    {
        _dataDragonMetadata = dataDragonMetadata;
    }
    
    public Uri BuildAvailableLanguagesUri()
    {
        UriBuilder uriBuilder = new UriBuilder(_dataDragonMetadata.BaseUri);

        uriBuilder.Path = "/cdn/languages.json";

        return uriBuilder.Uri;
    }

    public Uri BuildAllVersionsUri()
    {
        UriBuilder uriBuilder = new UriBuilder(_dataDragonMetadata.BaseUri);
        
        uriBuilder.Path = "/api/versions.json";
        
        return uriBuilder.Uri;
    }

    public Uri BuildChampionsUri(Core.Champion champion)
    {
        UriBuilder uriBuilder = new UriBuilder(_dataDragonMetadata.BaseUri);
        
        uriBuilder.Path = $"/cdn/img/champion/splash/{champion.Name}_0.jpg";
        
        return uriBuilder.Uri;
    }
    
    public Uri BuildSquareChampionsUri(Core.Champion champion)
    {
        UriBuilder uriBuilder = new UriBuilder(_dataDragonMetadata.BaseUri);
        
        uriBuilder.Path = $"/cdn/{champion.Version}/img/champion/{champion.Name}.png";
        
        return uriBuilder.Uri;
    }
}