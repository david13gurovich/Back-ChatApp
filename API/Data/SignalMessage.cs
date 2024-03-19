namespace API.Data
{
    public class SignalMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }

        public SignalMessage(string from, string to, string content)
        {
            From = from;
            To = to;
            Content = content;
        }
    }
}
