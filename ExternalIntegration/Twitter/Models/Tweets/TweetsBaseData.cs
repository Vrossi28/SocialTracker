using ExternalIntegration.Twitter.Interfaces.Tweets;

namespace ExternalIntegration.Twitter.Models.Tweets
{
    public class TweetsBaseData : ITweetsBaseData
    {
        public long Id { get; set; }
        public string Text { get; set; }
    }
}
