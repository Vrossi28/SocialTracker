using System;

namespace ExternalIntegration.Twitter.Interfaces.Tweets
{
    public interface ITweetsData : ITweetsBaseData
    {
        string Url { get; set; }
        string Author { get; set; }
        string CreatedAt { get; set; }
    }
}