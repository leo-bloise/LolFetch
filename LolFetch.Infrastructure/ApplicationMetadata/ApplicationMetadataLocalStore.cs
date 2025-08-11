using System.Text.Json;

using LolFetch.Application.Metadata;

namespace LolFetch.Infrastructure.ApplicationMetadata;

public class ApplicationMetadataLocalStore : IApplicationMetadataStore
{
    private static string APPLICATION_NAME = "LolFetch";
    
    private static string METADATA_FILENAME = "metadata.json";

    private string _appDataPath;
    
    public ApplicationMetadataLocalStore()
    {
        Initialize();
    }

    private void Initialize()
    {
        _appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), APPLICATION_NAME);
        
        Directory.CreateDirectory(_appDataPath);
    }

    public Metadata? Load()
    {
        string pathToFile = Path.Combine(_appDataPath, METADATA_FILENAME);
        
        return File.Exists(pathToFile) ? JsonSerializer.Deserialize<Metadata>(File.ReadAllText(Path.Combine(_appDataPath, METADATA_FILENAME))) : null;
    }

    public void Save(Metadata metadata)
    {
        string pathToFile = Path.Combine(_appDataPath, METADATA_FILENAME);
        
        File.WriteAllText(pathToFile, JsonSerializer.Serialize(metadata));
    }
}