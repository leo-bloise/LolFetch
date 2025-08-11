namespace LolFetch.Application;

/**
 * <summary>
 *  IVersionDownloader is responsible for downloading all versions of DataDragon or the latest version available.
 * </summary>
 */
public interface IVersionDownloader
{
    /**
     * <summary>
     * List all versions available from DataDragon
     * </summary>
     * <returns>
     *  Enumerable with all Versions available from DataDragon, except those not compatible with semantic versioning
     * </returns>
     */
    public Task<IEnumerable<Core.Version>> GetAllVersionsAsync();
    
    /**
     * <summary>Get the latest version available from DataDragon</summary>
     * <returns>
     *  Latest semantic versioning compatible version from DataDragon
     * </returns>
     */
    public Task<Core.Version> GetLatestVersion();
}