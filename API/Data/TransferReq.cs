namespace API.Data
{
    public class TransferReq
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }

        public TransferReq(string from, string to, string content)
        {
            From = from;
            To = to;
            Content = content;
        }
    }
}
