namespace LolFetch.Application.Metadata;

/**
 * <summary>
 * IApplicationMetadataStore is used to save and retrieve cached Metadata.
 * </summary>
 */
public interface IApplicationMetadataStore
{
    /**
     * <summary>
     *  Save and persist Metadata to cache
     * </summary> 
     */
    public void Save(Metadata metadata);

    /**
     * <summary>
     *  Load Metadata from cache, if it exists.
     * </summary>
     */
    public Metadata? Load();
}