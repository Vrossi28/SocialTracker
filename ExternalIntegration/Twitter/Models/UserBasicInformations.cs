using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalIntegration.Twitter.Models
{
    public class UserBasicInformations : IUserBasicInformations
    {
        public UserBaseData data { get; set; }
    }
}
