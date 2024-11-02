﻿// <auto-generated />
using System;
using FINALPROJECT.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FINALPROJECT.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240827175715_S1")]
    partial class S1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Address", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PostalCode")
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Auction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("AuctionEndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("AuctionStartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CarId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<double>("CurrentPrice")
                        .HasColumnType("double");

                    b.Property<string>("CustomerId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CustomerId1")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PaymentId")
                        .HasColumnType("longtext");

                    b.Property<string>("ShippingId")
                        .HasColumnType("longtext");

                    b.Property<double>("StartingPrice")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("CustomerId1");

                    b.ToTable("Auctions");
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Bid", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<string>("AuctionId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("AuctionId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Car", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ChasisNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Payment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<string>("AuctionId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ReferenceID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ShippingId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuctionId")
                        .IsUnique();

                    b.HasIndex("CustomerId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Shipping", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AddressId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AuctionId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PaymentReference")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TrackingNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("AuctionId")
                        .IsUnique();

                    b.HasIndex("CustomerId");

                    b.ToTable("Shipping");
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "01f3c",
                            DateCreated = new DateTime(2024, 8, 27, 17, 57, 15, 113, DateTimeKind.Utc).AddTicks(9656),
                            Email = "admin@gmail.com",
                            IsDeleted = false,
                            Name = "admin",
                            PasswordHash = "$2a$11$RpilI4N6g8rrLZXeSziUOeGW.O5odN8aYgGDtY4eTupSrch/O4TIy",
                            Role = 1,
                            Salt = "$2a$11$RpilI4N6g8rrLZXeSziUOe"
                        });
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Address", b =>
                {
                    b.HasOne("FINALPROJECT.Domain.Entities.Customer", "Customer")
                        .WithMany("Addresses")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Auction", b =>
                {
                    b.HasOne("FINALPROJECT.Domain.Entities.Car", "Car")
                        .WithMany("Auctions")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FINALPROJECT.Domain.Entities.Customer", null)
                        .WithMany("AuctionsPartaken")
                        .HasForeignKey("CustomerId");

                    b.HasOne("FINALPROJECT.Domain.Entities.Customer", null)
                        .WithMany("OutstandingPayments")
                        .HasForeignKey("CustomerId1");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Bid", b =>
                {
                    b.HasOne("FINALPROJECT.Domain.Entities.Auction", "Auction")
                        .WithMany("Bids")
                        .HasForeignKey("AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FINALPROJECT.Domain.Entities.Customer", "Customer")
                        .WithMany("BidsMade")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auction");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Customer", b =>
                {
                    b.HasOne("FINALPROJECT.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Payment", b =>
                {
                    b.HasOne("FINALPROJECT.Domain.Entities.Auction", "Auction")
                        .WithOne("Payment")
                        .HasForeignKey("FINALPROJECT.Domain.Entities.Payment", "AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FINALPROJECT.Domain.Entities.Customer", "Customer")
                        .WithMany("PaymentsMade")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auction");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Shipping", b =>
                {
                    b.HasOne("FINALPROJECT.Domain.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FINALPROJECT.Domain.Entities.Auction", "Auction")
                        .WithOne("Shipping")
                        .HasForeignKey("FINALPROJECT.Domain.Entities.Shipping", "AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FINALPROJECT.Domain.Entities.Customer", "Customer")
                        .WithMany("Shippings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Auction");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Auction", b =>
                {
                    b.Navigation("Bids");

                    b.Navigation("Payment");

                    b.Navigation("Shipping");
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Car", b =>
                {
                    b.Navigation("Auctions");
                });

            modelBuilder.Entity("FINALPROJECT.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("AuctionsPartaken");

                    b.Navigation("BidsMade");

                    b.Navigation("OutstandingPayments");

                    b.Navigation("PaymentsMade");

                    b.Navigation("Shippings");
                });
#pragma warning restore 612, 618
        }
    }
}
