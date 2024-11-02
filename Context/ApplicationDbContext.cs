using FINALPROJECT.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;
using System;
using Microsoft.EntityFrameworkCore;

namespace FINALPROJECT.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shipping> Shipping { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>()
        .HasKey(a => a.Id);
            builder.Entity<Auction>()
        .HasKey(a => a.Id);
                builder.Entity<Bid>()
        .HasKey(a => a.Id);
            builder.Entity<Car>()
        .HasKey(a => a.Id);
                    builder.Entity<Customer>()
        .HasKey(a => a.Id);
                    builder.Entity<Payment>()
        .HasKey(a => a.Id);
                    builder.Entity<Shipping>()
        .HasKey(a => a.Id);
                    builder.Entity<User>()
        .HasKey(a => a.Id);


            builder.Entity<Auction>()
       .HasOne(a => a.Payment)
       .WithOne(p => p.Auction)
       .HasForeignKey<Payment>(p => p.AuctionId)
       .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Auction>()
       .HasOne(a => a.Shipping)
       .WithOne(s => s.Auction)
       .HasForeignKey<Shipping>(s => s.AuctionId)
       .OnDelete(DeleteBehavior.Cascade);

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            builder.Entity<User>().HasData(new User
            {
                Email = "admin@gmail.com",
                Name = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin", salt),
                Role = Domain.Enums.Roles.Admin,
                Salt = salt,
                DateCreated = DateTime.UtcNow,
                Id = Guid.NewGuid().ToString().Substring(0, 5),
                IsDeleted = false,
            });
        }
    
    }
}

