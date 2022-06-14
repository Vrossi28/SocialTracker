using ExternalIntegration.Twitter.Interfaces.Tweets;
using ExternalIntegration.Twitter.Models.General;
using System.Collections.Generic;

namespace ExternalIntegration.Twitter.Models.Tweets
{
    public class TweetsBasicInformations : ITweetsBasicInformations
    {
        public List<TweetsBaseData> data { get; set; }
        public Meta meta { get; set; }
    }
}
