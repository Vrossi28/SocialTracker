using System.Collections.Generic;

namespace ExternalIntegration.Twitter.Models
{
    public class UserDescription : IUserDescription
    {
        public List<UrlDetails> urls { get; set; }
        public List<Hashtag> hashtags { get; set; }
    }

}
