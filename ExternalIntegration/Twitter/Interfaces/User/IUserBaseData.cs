namespace ExternalIntegration.Twitter.Models
{
    public interface IUserBaseData
    {
        long Id { get; set; }
        string Name { get; set; }
        string Username { get; set; }
    }
}