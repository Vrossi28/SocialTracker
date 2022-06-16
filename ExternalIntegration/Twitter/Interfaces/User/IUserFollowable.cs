using ExternalIntegration.Twitter.Models;
using ExternalIntegration.Twitter.Models.General;
using System.Collections.Generic;

namespace ExternalIntegration.Twitter.Interfaces.User
{
    public interface IUserFollowable
    {
        List<UserBaseData> Data { get; set; }
        Meta Meta { get; set; }
    }
}