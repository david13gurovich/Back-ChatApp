namespace API.Data
{
    public class NewContactItem
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
        public string Server { get; set; }

        public NewContactItem(string id, string server)
        {
            Id = id;
            Server = server;
        }
    }
}
