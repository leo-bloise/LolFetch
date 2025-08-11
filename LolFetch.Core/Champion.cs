namespace LolFetch.Core;

/**
 * <summary>
 * League of Legends champion. Each champion is composed by a version number and a name. The name is very unlikely to change, but the version will vary a lot. Every version may
 * cause the champion aspects to change, such like images, spells and so on. Then, it's very important to specify name and version.
 * </summary>
 */
public record Champion
{
    public string Name { get; init; }
     
    public Version Version { get; init; }

    public Champion(string name, Version version)
    {
        ValidateChampionName(name);
        
        Name = name;
        Version = version;
    }

    private void ValidateChampionName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
    }
}