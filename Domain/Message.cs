using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    //public enum Type
    //{
    //    text,
    //    video,
    //    image,
    //    audio
    //}
    public class Message
    {
        [Key]
        public int Id { get; set; }
        //from to ?

        public string? From { get; set; }

        public string? To { get; set; }

        public string? Type { get; set; }
        
        public string Content { get; set; }
        public DateTime? Created { get; set; }

        public bool Sent { get; set; }

    }
}
