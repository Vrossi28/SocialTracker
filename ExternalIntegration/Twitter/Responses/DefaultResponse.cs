namespace ExternalIntegration.Twitter.Responses
{
    public class DefaultResponse<T>
    {
        public object Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
