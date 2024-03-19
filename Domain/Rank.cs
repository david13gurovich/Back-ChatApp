using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Rank
    {
        [Key]
        public string UserName { get; set; }

        [Range(1, 5)]
        public int RankNum { get; set; }
        public string? Details { get; set; }

        public DateTime? Created { get; set; }
    }
}
