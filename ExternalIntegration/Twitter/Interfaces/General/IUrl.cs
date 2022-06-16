using System.Collections.Generic;

namespace ExternalIntegration.Twitter.Models
{
    public interface IUrl
    {
        List<UrlDetails> Urls { get; set; }
    }
}