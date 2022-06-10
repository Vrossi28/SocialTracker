namespace ExternalIntegration.Twitter.Models
{
    public class Hashtag : IHashtag
    {
        public int start { get; set; }
        public int end { get; set; }
        public string tag { get; set; }
    }

}
