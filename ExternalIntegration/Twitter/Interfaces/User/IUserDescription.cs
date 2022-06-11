using System.Collections.Generic;

namespace ExternalIntegration.Twitter.Models
{
    public interface IUserDescription
    {
        List<Hashtag> hashtags { get; set; }
        List<UrlDetails> urls { get; set; }
    }
}