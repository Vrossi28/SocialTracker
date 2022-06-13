using ExternalIntegration.Twitter.Interfaces.Tweets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalIntegration.Twitter.Models.Tweets
{
    public class TweetsBaseData : ITweetsBaseData
    {
        public long Id { get; set; }
        public string Text { get; set; }
    }
}
