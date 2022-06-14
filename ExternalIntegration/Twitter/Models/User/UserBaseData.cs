namespace ExternalIntegration.Twitter.Models
{
    public class UserBaseData : IUserBaseData
    {
        public long id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
    }

}
