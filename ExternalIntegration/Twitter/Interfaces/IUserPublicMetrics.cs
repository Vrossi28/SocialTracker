namespace ExternalIntegration.Twitter.Models
{
    public interface IUserPublicMetrics
    {
        int followers_count { get; set; }
        int following_count { get; set; }
        int listed_count { get; set; }
        int tweet_count { get; set; }
    }
}