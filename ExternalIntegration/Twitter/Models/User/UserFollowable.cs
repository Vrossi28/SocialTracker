using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalIntegration.Twitter.Interfaces.User;
using ExternalIntegration.Twitter.Models.General;
using Newtonsoft.Json;

namespace ExternalIntegration.Twitter.Models.User
{
    public class UserFollowable : IUserFollowable
    {
        [JsonProperty(PropertyName = "data")]
        public List<UserBaseData> Data { get; set; }
        [JsonProperty(PropertyName = "meta")]
        public Meta Meta { get; set; }
    }
}
