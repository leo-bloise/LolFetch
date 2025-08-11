namespace LolFetch.Application.Metadata;

/**
 * <summary>
 * Metadata is used to represent application metadata needed. This object can be cached locally or remotely for reuse.
 *
 * It also defines a Expired and ExpirationDate runtime property. ExpirationDate is always one day after the CreationDate and
 * Expired checks every time it's accessed to determine if this Metadata is expired or not.
 * </summary>
 */
public record Metadata(string Version, DateTime CreationDate)
{
    private DateTime ExpirationDate => CreationDate.AddDays(1);
    
    public bool Expired { get => DateTime.Now > ExpirationDate; }
}