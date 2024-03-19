using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        //user name of contact
        public string ContactName { get; set; }
        //user name belong to
        public  string? UserName { get; set; }
        //nickname
        public string NickName { get; set; }
        public string Server { get; set; }

        //last message
        //can be null
        public string? Last { get; set; }
        //can be null

        public DateTime? LastDate { get; set; }

        //public List<Message>? MessageList { get; set; }




    }
}
