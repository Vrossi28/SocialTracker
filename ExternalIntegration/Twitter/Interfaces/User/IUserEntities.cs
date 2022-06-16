namespace ExternalIntegration.Twitter.Models
{
    public interface IUserEntities
    {
        UserDescription Description { get; set; }
        Url Url { get; set; }
    }
}