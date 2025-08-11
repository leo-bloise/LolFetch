using LolFetch.Application;
using LolFetch.Application.Metadata;
using LolFetch.Infrastructure.ApplicationMetadata;
using Moq;

namespace LolFetch.Infrastructure.Test;

public class ApplicationMetadataRepositoryTest
{
    private readonly IApplicationMetadataRepository _applicationMetadataRepository;
    
    private readonly Mock<IVersionDownloader> _mockVersionDownloader;
    
    private readonly Mock<IApplicationMetadataStore> _mockApplicationMetadataStore;

    public ApplicationMetadataRepositoryTest()
    {
        _mockVersionDownloader = new Mock<IVersionDownloader>();
        _mockApplicationMetadataStore = new Mock<IApplicationMetadataStore>();
        
        _applicationMetadataRepository = new ApplicationMetadataRepository(_mockVersionDownloader.Object, _mockApplicationMetadataStore.Object);
    }

    [Fact]
    public async Task FindOrCreateLatestAsync_DownloadVersionFromDataDragon_ReturnsMetadata()
    {
        _mockApplicationMetadataStore.Setup(it => it.Load()).Returns(() => null);
        
        _mockVersionDownloader.Setup(it => it.GetLatestVersion()).Returns(() => Task.FromResult(Core.Version.Parse("1.0.0")));
        
        Metadata metadata = await _applicationMetadataRepository.FindOrCreateLatestAsync();
        
        Assert.NotNull(metadata);
        
        Assert.Equal("1.0.0", metadata.Version);
        
        Assert.Equal(metadata.CreationDate.Day, DateTime.Today.Day);
        Assert.Equal(metadata.CreationDate.Month, DateTime.Today.Month);
        Assert.Equal(metadata.CreationDate.Year, DateTime.Today.Year);
        
        _mockApplicationMetadataStore.Verify(it => it.Save(It.IsAny<Metadata>()), Times.Once);
    }
    
    [Fact]
    public async Task FindOrCreateLatestAsync_GetFromLocalCache_ReturnsMetadata()
    {
        Metadata metadataMocked = new Metadata("1.0.0", DateTime.Now);
        _mockApplicationMetadataStore.Setup(it => it.Load()).Returns(() => metadataMocked);
        
        Metadata metadata = await _applicationMetadataRepository.FindOrCreateLatestAsync();
        
        Assert.Equal(metadataMocked, metadata);
        
        _mockApplicationMetadataStore.Verify(it => it.Load(), Times.Once);
        _mockApplicationMetadataStore.Verify(it => it.Save(It.IsAny<Metadata>()), Times.Never);
    }
}