using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }

        //public List<Contact>? ContactsList { get; set; }

    }
}
