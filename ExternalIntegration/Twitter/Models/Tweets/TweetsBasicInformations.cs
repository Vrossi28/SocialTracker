using ExternalIntegration.Twitter.Interfaces.Tweets;
using ExternalIntegration.Twitter.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalIntegration.Twitter.Models.Tweets
{
    public class TweetsBasicInformations : ITweetsBasicInformations
    {
        public List<TweetsBaseData> data { get; set; }
        public Meta meta { get; set; }
    }
}
