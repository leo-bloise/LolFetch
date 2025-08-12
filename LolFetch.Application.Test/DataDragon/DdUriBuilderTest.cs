using LolFetch.Application.DataDragon;
using LolFetch.Core.DataDragon;

namespace LolFetch.Application.Test.DataDragon;

public sealed class DdUriBuilderTest
{
    private static readonly string BASE_URI = "https://www.test.com";
    
    private readonly DdUriBuilder _ddUriBuilder = new(
        new DataDragonMetadata(
            new Uri(BASE_URI)
            )
        );
    
    [Fact]
    public void BuildAvailableLanguagesUri_PredefinedBaseTestUrl_ReturnsAvailableLanguagesUriAppendedToBaseTestUrl()
    {
        Uri result = _ddUriBuilder.BuildAvailableLanguagesUri();

        Uri languageUriExpected = new Uri("https://www.test.com/cdn/languages.json"); 
        
        Assert.Equal(languageUriExpected, result);
    }

    [Fact]
    public void BuildAllVersionsUri_PredefinedBaseTestUrl_ReturnsAllVersionUriAppendedToBaseTestUrl()
    {
        Uri result = _ddUriBuilder.BuildAllVersionsUri();
        
        Uri allVersionsUriExpected = new Uri("https://www.test.com/api/versions.json");
        
        Assert.Equal(allVersionsUriExpected, result);
    }
}