using ExternalIntegration.Twitter.Models.General;
using ExternalIntegration.Twitter.Models.Tweets;
using System.Collections.Generic;

namespace ExternalIntegration.Twitter.Interfaces.Tweets
{
    public interface ITweetsBasicInformations
    {
        List<TweetsBaseData> data { get; set; }
        Meta meta { get; set; }
    }
}