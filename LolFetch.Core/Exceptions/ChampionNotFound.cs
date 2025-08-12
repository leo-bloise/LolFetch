namespace LolFetch.Core.Exceptions;

public class ChampionNotFound : Exception
{
    public ChampionNotFound(Champion champion): base($"Champion {champion.Name} and version {champion.Version} not found") {}
}