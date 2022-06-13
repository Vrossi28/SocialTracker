namespace ExternalIntegration.Twitter.Interfaces.Tweets
{
    public interface ITweetsAllData
    {
        string Author { get; set; }
        string CreatedAt { get; set; }
        string Id { get; set; }
        string Text { get; set; }
        string Url { get; set; }
    }
}