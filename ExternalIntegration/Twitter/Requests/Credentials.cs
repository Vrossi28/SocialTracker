namespace ExternalIntegration.Twitter.Requests
{
    public static class Credentials
    {
        public static string Bearer { get; private set; } = "BEARER_TOKEN";
        public static string ConsumerKey { get; private set; } = "CONSUMER_KEY";
        public static string ConsumerKeySecret { get; private set; } = "CONSUMER_SECRET";
        public static string AccessToken { get; private set; } = "ACCESS_TOKEN";
        public static string AccessTokenSecret { get; private set; } = "ACCESS_TOKEN_SECRET";
        public static string ClientId { get; private set; } = "CLIENT_ID";
        public static string ClientIdSecret { get; private set; } = "CLIENT_ID_SECRET";
        public static string IdUser { get; private set; } = "ID_USER";

    }
}
