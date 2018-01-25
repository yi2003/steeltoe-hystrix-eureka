using aaa;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aaaaa
{
    public class SimpleDb:DbContext
    {
        public SimpleDb(DbContextOptions<SimpleDb> options) : base(options)
        {

        }


        public DbSet<RecordCount> RecordCounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecordCount>()
               .ToTable("RecordCount")
               .HasKey(ur => ur.Id);
        }
    }
}
