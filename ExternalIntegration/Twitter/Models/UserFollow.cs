using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalIntegration.Twitter.Models
{
    public class UserFollow : IUserFollow
    {
        public UserFollowData data { get; set; }
    }
}
