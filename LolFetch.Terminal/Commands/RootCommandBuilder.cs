using System.CommandLine;

namespace LolFetch.Commands;

public class RootCommandBuilder(LolFetchCommand lolFetchCommand)
{
    public RootCommand BuildCommand()
    {
        RootCommand rootCommand =
            new RootCommand("Render your favorite splash art of League of Legends champions in ASCII")
            {
                Options =
                {
                    lolFetchCommand.ColorOption,
                    lolFetchCommand.SquareOption
                },
                Arguments =
                {
                    lolFetchCommand.ChampionName
                },
            };
        
        rootCommand.SetAction(lolFetchCommand.HandleAsync);

        return rootCommand;
    }
}