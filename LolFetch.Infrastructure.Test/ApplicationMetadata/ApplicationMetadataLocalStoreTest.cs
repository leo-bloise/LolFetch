using LolFetch.Application.Metadata;
using LolFetch.Infrastructure.ApplicationMetadata;

namespace LolFetch.Infrastructure.Test.ApplicationMetadata;

public class ApplicationMetadataLocalStoreTest
{
    private readonly IApplicationMetadataStore _applicationMetadataStore;

    public ApplicationMetadataLocalStoreTest()
    {
        _applicationMetadataStore = new ApplicationMetadataLocalStore();
    }
    
    [Fact]
    public void Load_NotExistMetadata_ReturnsNull()
    {
        Metadata? metadataExtractedLocally = _applicationMetadataStore.Load();
        
        Assert.Null(metadataExtractedLocally);
    }

    [Fact]
    public void Load_SaveMetadataAndExtract_ReturnsMetadata()
    {
        Metadata metadatExpected = new Metadata("test", DateTime.Now);
        
        _applicationMetadataStore.Save(metadatExpected);
        
        Metadata? metadataExtracted = _applicationMetadataStore.Load();
        
        Assert.NotNull(metadataExtracted);
        
        Assert.Equal(metadatExpected, metadataExtracted);
        
        Assert.False(metadataExtracted.Expired);
    }
}