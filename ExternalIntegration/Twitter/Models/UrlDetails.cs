namespace ExternalIntegration.Twitter.Models
{
    public class UrlDetails : IUrlDetails
    {
        public int start { get; set; }
        public int end { get; set; }
        public string url { get; set; }
        public string expanded_url { get; set; }
        public string display_url { get; set; }
    }

}
