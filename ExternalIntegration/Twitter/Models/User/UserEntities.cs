using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExternalIntegration.Twitter.Models
{
    [Keyless]
    [NotMapped]
    public class UserEntities : IUserEntities
    {
        [JsonProperty(PropertyName = "url")]
        public Url Url { get; set; }
        [JsonProperty(PropertyName = "description")]
        public UserDescription Description { get; set; }
    }

}
