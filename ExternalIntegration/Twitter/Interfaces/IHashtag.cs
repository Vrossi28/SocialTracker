namespace ExternalIntegration.Twitter.Models
{
    public interface IHashtag
    {
        int end { get; set; }
        int start { get; set; }
        string tag { get; set; }
    }
}