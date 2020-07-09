namespace ApiPushTestReceiver
{
    public class AppSettings
    {
        public int BufferSize { get; set; } = 40;
        public string ApiKeyHeaderName { get; set; }
        public string ValidApiKey { get; set; }
    }
}