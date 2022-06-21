using ExternalIntegration.Twitter.Interfaces.Tweets;
using ExternalIntegration.Twitter.Interfaces.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalIntegration.Twitter.Models.User
{
    public class RetweetsData : IRetweetsData
    {
        public ITweetsAllData Data { get; set; }
        [JsonProperty(PropertyName = "data")]
        public Retweets Status { get; set; }
    }
}
