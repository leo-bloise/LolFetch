namespace LolFetch.Core.DataDragon;

/**
 * <summary>
 * DataDragon is the Riot's main API that provides access to image resources. DataDragonMetadata provides the basic metadata needed to access this API
 * </summary>
 */
public record DataDragonMetadata(Uri BaseUri);