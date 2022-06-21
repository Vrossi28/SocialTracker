using ExternalIntegration.Twitter.Interfaces.Tweets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalIntegration.Twitter.Models.User
{
    public class Retweets
    {
        [JsonProperty(PropertyName = "retweeted")]
        public bool Retweeted { get; set; }
    }
}
