namespace LolFetch.Application.Metadata;

/**
 * <summary>
 *  IApplicationMetadataRepository is the service provided to always retrieve the lastest Metadata available.
 * </summary>
 */
public interface IApplicationMetadataRepository
{
    /**
     * <summary>
     * Retrieves the latest Metadata available. If there isn't one valid, then it'll be created and returned. The CreationDate will be configured as
     * DateTime.Now.
     * </summary>
     * <returns>
     *  Metadata created or retrieved.
     * </returns>
     */
    public Task<Metadata> FindOrCreateLatestAsync();
}