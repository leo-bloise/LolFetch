using LolFetch.Application;
using LolFetch.Application.Champion;
using LolFetch.Application.DataDragon;
using LolFetch.Application.Metadata;
using LolFetch.Application.Output;
using LolFetch.Commands;
using LolFetch.Core.DataDragon;
using LolFetch.Infrastructure;
using LolFetch.Infrastructure.ApplicationMetadata;
using LolFetch.Infrastructure.Output;

namespace LolFetch;

public class Program
{
    public static int Main(string[] args)
    {
        using HttpClient httpClient = new HttpClient();
        
        DataDragonMetadata dataDragonMetadata = new DataDragonMetadata(new Uri("https://ddragon.leagueoflegends.com"));
        DdUriBuilder ddUriBuilder = new DdUriBuilder(dataDragonMetadata);
        IVersionDownloader versionDownloader = new VersionDownloader(ddUriBuilder, httpClient);
        IApplicationMetadataStore applicationMetadataStore = new ApplicationMetadataLocalStore();
        IApplicationMetadataRepository applicationMetadataRepository = new ApplicationMetadataRepository(versionDownloader, applicationMetadataStore);
        ICanvasOutputDevice canvasOutputDevice = new CanvasOutputTerminal();

        LolFetchCommand command = new LolFetchCommand(
            new RenderChampionUseCase(
                new ChampionImageDownloader(ddUriBuilder), new AscIIDraftsman()
                ), 
            applicationMetadataRepository, 
            canvasOutputDevice
            );
        
        RootCommandBuilder rootCommandBuilder = new RootCommandBuilder(command);

        return rootCommandBuilder
            .BuildCommand()
            .Parse(args).Invoke();
    }
}