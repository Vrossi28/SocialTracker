namespace ExternalIntegration.Twitter.Models
{
    public interface IUrlDetails
    {
        string DisplayUrl { get; set; }
        int End { get; set; }
        string ExpandedUrl { get; set; }
        int Start { get; set; }
        string Url { get; set; }
    }
}