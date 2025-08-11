namespace LolFetch.Core.Exceptions;

public class InvalidChampionVersion : Exception
{
    public InvalidChampionVersion(string version) : base($"Version {version} is invalid.") { }
}