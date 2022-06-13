namespace ExternalIntegration.Twitter.Interfaces.Tweets
{
    public interface ITweetsAllData
    {
        string Author { get; set; }
        string CreatedAt { get; set; }
        long Id { get; set; }
        string Text { get; set; }
        string Url { get; set; }
    }
}