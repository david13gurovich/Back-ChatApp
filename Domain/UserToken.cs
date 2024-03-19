using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
     public class UserToken
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }


    }
}
