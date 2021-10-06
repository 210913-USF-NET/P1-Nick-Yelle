using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DL
{
    public class P1DBContext : DbContext
    {
        public P1DBContext() : base() { }
        public P1DBContext(DbContextOptions options) : base(options) {}

        public DbSet<Brew> Brews { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        //    protected override void OnModelCreating(ModelBuilder modelBuilder)
        //    {
        //       modelBuilder.Entity<Restaurant>()
        //       .Property(restaurant => restaurant.Id)
        //       .ValueGeneratedOnAdd();

        //       modelBuilder.Entity<Review>()
        //       .Property(review => review.Id)
        //       .ValueGeneratedOnAdd();
        //    }
    }
}
