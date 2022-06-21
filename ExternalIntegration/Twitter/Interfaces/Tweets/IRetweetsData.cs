using ExternalIntegration.Twitter.Interfaces.Tweets;
using ExternalIntegration.Twitter.Models.User;

namespace ExternalIntegration.Twitter.Interfaces.User
{
    public interface IRetweetsData
    {
        ITweetsAllData Data { get; set; }
        Retweets Status { get; set; }
    }
}