namespace ExternalIntegration.Twitter.Models
{
    public interface IHashtag
    {
        int End { get; set; }
        int Start { get; set; }
        string Tag { get; set; }
    }
}