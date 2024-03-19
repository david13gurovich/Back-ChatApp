namespace API.Data
{
    public class ContactInfo
    {
        public string Id { get; set; }
        public string Server { get; set; }

        public ContactInfo(string id, string server)
        {
            Id = id;
            Server = server;
        }
    }
}
