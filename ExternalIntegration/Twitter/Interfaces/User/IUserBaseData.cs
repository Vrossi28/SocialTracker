namespace ExternalIntegration.Twitter.Models
{
    public interface IUserBaseData
    {
        long id { get; set; }
        string name { get; set; }
        string username { get; set; }
    }
}