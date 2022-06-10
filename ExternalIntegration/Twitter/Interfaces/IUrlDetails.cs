namespace ExternalIntegration.Twitter.Models
{
    public interface IUrlDetails
    {
        string display_url { get; set; }
        int end { get; set; }
        string expanded_url { get; set; }
        int start { get; set; }
        string url { get; set; }
    }
}