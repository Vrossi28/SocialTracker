using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalIntegration.Twitter.Models.General
{
    public class Meta
    {
        public string next_token { get; set; }
        public int result_count { get; set; }
        public string newest_id { get; set; }
        public string oldest_id { get; set; }
    }
}
