namespace ExternalIntegration.Twitter.Models
{
    public interface IUserEntities
    {
        UserDescription description { get; set; }
        Url url { get; set; }
    }
}