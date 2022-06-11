namespace ExternalIntegration.Twitter.Models
{
    public class UserPublicMetrics : IUserPublicMetrics
    {
        public int followers_count { get; set; }
        public int following_count { get; set; }
        public int tweet_count { get; set; }
        public int listed_count { get; set; }
    }

}
