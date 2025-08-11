using LolFetch.Application;
using LolFetch.Application.Metadata;

namespace LolFetch.Infrastructure.ApplicationMetadata;

public class ApplicationMetadataRepository : IApplicationMetadataRepository
{
    private readonly IVersionDownloader _versionDownloader;
    
    private readonly IApplicationMetadataStore _applicationMetadataLocalStore;

    public ApplicationMetadataRepository(IVersionDownloader versionDownloader, IApplicationMetadataStore applicationMetadataLocalStore)
    {
        _versionDownloader = versionDownloader;
        _applicationMetadataLocalStore = applicationMetadataLocalStore;
    }
    
    public async Task<Metadata> FindOrCreateLatestAsync()
    {
        Metadata? metadata = _applicationMetadataLocalStore.Load();

        if (metadata != null && !metadata.Expired) return metadata;

        Core.Version version = await _versionDownloader.GetLatestVersion();

        return Create(version, DateTime.Now);
    }

    public Metadata Create(Core.Version version, DateTime creationDate)
    {
        Metadata metadata = new Metadata(version.ToString(), creationDate);
        
        _applicationMetadataLocalStore.Save(metadata);
        
        return metadata;
    }
}