using ExternalIntegration.Twitter.Interfaces.Tweets;

namespace ExternalIntegration.Twitter.Models.Tweets
{
    public class TweetsData : ITweetsAllData
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string CreatedAt { get; set; }
        public string Url { get; set; }
    }
}
