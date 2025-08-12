using LolFetch.Application.Champion;
using LolFetch.Core;
using Moq;
using Version = LolFetch.Core.Version;

namespace LolFetch.Application.Test.Champion;

public class RenderChampionUseCaseTest
{
    private readonly RenderChampionUseCase _renderChampionUseCase;
    
    private readonly Mock<IChampionImageDownloader>  _championImageDownloaderMock;
    
    private readonly Mock<IAscIIDraftsman> _asciiDraftsmanMock;

    public RenderChampionUseCaseTest()
    {
        _championImageDownloaderMock  = new Mock<IChampionImageDownloader>();
        _asciiDraftsmanMock  = new Mock<IAscIIDraftsman>();
        
        _renderChampionUseCase = new RenderChampionUseCase(
            _championImageDownloaderMock.Object,
            _asciiDraftsmanMock.Object
            );
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task RenderChampionAsync_Success_DownloadsSplashingScreenAndDrawsImageToCanvas(bool useColors)
    {
        Version version = new Version("0", "0", "1");
        Canvas canvas = new Canvas(100, 100);
        RenderChampionRequest renderChampionRequest = new RenderChampionRequest(new Core.Champion("test", version), canvas, useColors, false);
        
        _championImageDownloaderMock
            .Setup(it => it.DownloadLoadingSplashArt(It.IsAny<Core.Champion>()))
            .ReturnsAsync(new byte[10]);
        
        await _renderChampionUseCase.RenderChampionAsync(renderChampionRequest);

        _championImageDownloaderMock.Verify(it => it.DownloadLoadingSplashArt(It.IsAny<Core.Champion>()), Times.Once);
        _championImageDownloaderMock.Verify(it => it.DownloadSquareSplashArt(It.IsAny<Core.Champion>()), Times.Never);
        _asciiDraftsmanMock.Verify(it => it.DrawAscii(It.IsAny<byte[]>(), canvas, useColors), Times.Once);
    }
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task RenderChampionAsync_Success_DownloadsSquareAndDrawsImageToCanvas(bool useColors)
    {
        Version version = new Version("0", "0", "1");
        Canvas canvas = new Canvas(100, 100);
        RenderChampionRequest renderChampionRequest = new RenderChampionRequest(new Core.Champion("test", version), canvas, useColors, true);
        
        _championImageDownloaderMock
            .Setup(it => it.DownloadSquareSplashArt(It.IsAny<Core.Champion>()))
            .ReturnsAsync(new byte[10]);
        
        await _renderChampionUseCase.RenderChampionAsync(renderChampionRequest);

        _championImageDownloaderMock.Verify(it => it.DownloadLoadingSplashArt(It.IsAny<Core.Champion>()), Times.Never);
        _championImageDownloaderMock.Verify(it => it.DownloadSquareSplashArt(It.IsAny<Core.Champion>()), Times.Once);
        _asciiDraftsmanMock.Verify(it => it.DrawAscii(It.IsAny<byte[]>(), canvas, useColors), Times.Once);
    }
}