using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalIntegration.Twitter.Interfaces.User;
using ExternalIntegration.Twitter.Models.General;

namespace ExternalIntegration.Twitter.Models.User
{
    public class UserFollowable : IUserFollowable
    {
        public List<UserBaseData> data { get; set; }
        public Meta meta { get; set; }
    }
}
