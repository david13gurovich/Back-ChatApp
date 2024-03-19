using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

    public class PomeloDB : DbContext
    {
        public PomeloDB (DbContextOptions<PomeloDB> options)
            : base(options)
        {
        }

    public DbSet<Domain.Contact>? Contact { get; set; }

    public DbSet<Domain.User>? User { get; set; }

    public DbSet<Domain.Message>? Message { get; set; }

    public DbSet<Domain.Rank>? Rank { get; set; }
}
