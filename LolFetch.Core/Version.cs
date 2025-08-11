using LolFetch.Core.Exceptions;

namespace LolFetch.Core;
/**
 * <summary>
 * Version is a value object that implements the semantic versioning pattern. Every version must have a Major, Minor and Patch number separated by dots. For example, 0.0.1 is a valid semantic version while 0.1 is not.
 * </summary>
 */
public record Version
{
    public string Major { get; }
    
    public string Minor { get; }
    
    public string Patch { get; }

    public Version(string major, string minor, string patch)
    {
        ValidateNumericString(major);
        ValidateNumericString(minor);
        ValidateNumericString(patch);
        
        Major = major;
        Minor = minor;
        Patch = patch;
    }
    
    /**
     * <summary>
     * Parses a string into a semantic version object. The string must fulfill the following regex: \d+.\d+.\d+
     * </summary>
     * <returns>Version object extracted from the string</returns>
     * <param name="version">String holding a semantic version representation</param>
     * <exception cref="InvalidChampionVersion">Version string doesn't fulfill expected format \d+.\d+.\d+</exception>
     */
    public static Version Parse(string version)
    {
        string[] parts = version.Split(".");

        if (parts.Length != 3) throw new InvalidChampionVersion(version);

        return new Version(parts[0], parts[1], parts[2]);
    }
    private void ValidateNumericString(string text)
    {
        if (!int.TryParse(text, out _)) throw new FormatException($"Major, minor or patch must be a numeric text, not {text}");
    }

    public override string ToString()
    {
        return $"{Major}.{Minor}.{Patch}";
    }
}