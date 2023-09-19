using MblexApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MblexApp.Context
{
    public class MyDbContext:DbContext
    {
        public DbSet<Question> Questions { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define any additional configuration for your entities here
            modelBuilder.Entity<Question>()
                .HasKey(q => q.Id);
            // You can configure other entity properties, relationships, etc. here
        }
    }
}
