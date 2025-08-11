using System.Globalization;
using System.Text.RegularExpressions;

namespace LolFetch.Application.Champion;

public class ChampionNameFormatter
{
    private Dictionary<string, string> _predefinedNames = new Dictionary<string, string>();

    public ChampionNameFormatter()
    {
        InitializePredefinedNames();
    }
    
    private void InitializePredefinedNames()
    {
        _predefinedNames["Reksai"] = "RekSai";
    }
    
    public string Format(string championName)
    {
        return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Regex.Replace(championName, @"[^a-zA-Z]", ""));
    }
}