using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExternalIntegration.Twitter.Models
{
    [Keyless]
    public class Url : IUrl
    {
        [JsonProperty(PropertyName = "urls")]
        public List<UrlDetails> Urls { get; set; }
    }

}
