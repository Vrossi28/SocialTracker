using System.Collections.Generic;

namespace ExternalIntegration.Twitter.Models
{
    public interface IUrl
    {
        List<UrlDetails> urls { get; set; }
    }
}