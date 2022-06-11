using System.Collections.Generic;

namespace ExternalIntegration.Twitter.Models
{
    public class UserEntities : IUserEntities
    {
        public Url url { get; set; }
        public UserDescription description { get; set; }
    }

}
