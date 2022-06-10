using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalIntegration.Twitter.Models
{
    public class UserAllData : IUserBaseData, IUserAllData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public UserPublicMetrics public_metrics { get; set; }
        public DateTime created_at { get; set; }
        public string description { get; set; }
        public bool verified { get; set; }
        public string url { get; set; }
        public bool @protected { get; set; }
        public string pinned_tweet_id { get; set; }
        public string profile_image_url { get; set; }
        public UserEntities entities { get; set; }
        public string location { get; set; }
    }
}
