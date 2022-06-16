using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExternalIntegration.Twitter.Models
{
    public class Url : IUrl
    {
        [JsonProperty(PropertyName = "urls")]
        public List<UrlDetails> Urls { get; set; }
    }

}
