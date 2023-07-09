using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace Order.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {

        }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Provider> Providers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Provider a = new Provider { Id = 1, Name = "First" };
            Provider b = new Provider { Id = 2, Name = "Second" };
            Provider c = new Provider { Id = 3, Name = "Third" };
            Provider d = new Provider { Id = 4, Name = "Fourth" };
            modelBuilder.Entity<Provider>().HasData(new Provider[] { a, b, c, d });
            base.OnModelCreating(modelBuilder);
        }



    }
}
