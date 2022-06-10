using System.Collections.Generic;

namespace ExternalIntegration.Twitter.Models
{
    public class Url : IUrl
    {
        public List<UrlDetails> urls { get; set; }
    }

}
