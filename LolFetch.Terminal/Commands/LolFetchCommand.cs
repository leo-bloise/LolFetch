using System.CommandLine;
using LolFetch.Application;
using LolFetch.Application.Champion;
using LolFetch.Application.DataDragon;
using LolFetch.Application.Metadata;
using LolFetch.Application.Output;
using LolFetch.Core;
using LolFetch.Infrastructure;

namespace LolFetch.Commands;

public class LolFetchCommand(RenderChampionUseCase renderChampionUseCase, IApplicationMetadataRepository applicationMetadataRepository, ICanvasOutputDevice canvasOutputDevice)
{
    public readonly Argument<string> ChampionName = new ("champion")
    {
        Description = "champion to be rendered in ASCII",
    };
    
    public readonly Option<bool> SquareOption = new("--square")
    {
        Description = "Square image of champion instead of splash art",
        Required = false
    };

    public readonly Option<bool> ColorOption = new("--color")
    {
        Description = "Color image of champion instead of grayscale",
        Required = false
    };

    private async Task<RenderChampionRequest> CreateRequest(ParseResult parseResult)
    {
        Metadata metadata = await applicationMetadataRepository.FindOrCreateLatestAsync();
        
        string championNameValue = parseResult.GetRequiredValue(ChampionName);
        
        Canvas canvas = new Canvas(Console.WindowHeight, Console.WindowWidth);
        
        Champion champion = new Champion(championNameValue, Core.Version.Parse(metadata.Version));
        
        bool colorValue = parseResult.GetValue(ColorOption);
        bool squareValue = parseResult.GetValue(SquareOption);

        return new RenderChampionRequest(champion, canvas, colorValue, squareValue);
    }
    
    public async Task HandleAsync(ParseResult parseResult)
    {
        RenderChampionRequest request = await CreateRequest(parseResult);
        
        await renderChampionUseCase.RenderChampionAsync(request);
        
        canvasOutputDevice.Render(request.Canvas);
    }
}