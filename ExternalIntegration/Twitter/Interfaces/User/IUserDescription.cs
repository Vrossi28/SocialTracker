using System.Collections.Generic;

namespace ExternalIntegration.Twitter.Models
{
    public interface IUserDescription
    {
        List<Hashtag> Hashtags { get; set; }
        List<UrlDetails> Urls { get; set; }
    }
}