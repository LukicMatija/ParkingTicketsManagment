using Microsoft.EntityFrameworkCore;
using ParkingTicketsManagment.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SubscriptionTicket> SubscriptionTickets { get; set; }
        public DbSet<Payment>Payments { get; set; }
        public DbSet<ParkingTicket>ParkingTickets { get; set; }
        public DbSet<Location>Locations { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired();

                entity.Property(u => u.Role)
                    .IsRequired()
                    .HasConversion<string>();

                entity.HasMany(u => u.Vehicles)
                    .WithOne(v => v.User)
                    .HasForeignKey(v => v.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<Zone>(entity =>
            {

                entity.HasKey(z => z.Id);
                entity.Property(z => z.Name).IsRequired().HasMaxLength(50);
                entity.Property(z => z.PricePerHour).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(z => z.ZoneBoundaries).IsRequired();

                entity.HasMany(z => z.Locations)
                      .WithOne(l => l.Zone)
                      .HasForeignKey(l => l.ZoneId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(z => z.SubscriptionTickets)
                      .WithOne(st => st.Zone)
                      .HasForeignKey(st => st.ZoneId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(v => v.Id);

                entity.Property(v => v.LicensePlate).IsRequired().HasMaxLength(20);
                entity.Property(v => v.Make).IsRequired().HasMaxLength(50);
                entity.Property(v => v.Model).IsRequired().HasMaxLength(50);

                entity.HasOne(v => v.User)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(v => v.SubscriptionTickets)
                .WithOne(v => v.Vehicle)
                .HasForeignKey(v => v.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(v => v.ParkingTickets)
                .WithOne(v => v.Vehicle)
                .HasForeignKey(v => v.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<SubscriptionTicket>(entity =>
            {
                entity.HasKey(st => st.Id);

                entity.Property(st => st.ValidFrom).IsRequired();
                entity.Property(st => st.ValidTo).IsRequired();
                entity.Property(st => st.Price).IsRequired().HasColumnType("decimal(18,2)");

                entity.HasOne(st => st.Vehicle)
                .WithMany(st => st.SubscriptionTickets)
                .HasForeignKey(st => st.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(st => st.Zone)
                .WithMany(st => st.SubscriptionTickets)
                .HasForeignKey(st => st.ZoneId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(l => l.Id);

                entity.Property(l => l.StreetName).IsRequired().HasMaxLength(100);
                entity.Property(l => l.City).IsRequired().HasMaxLength(50);
                entity.Property(l => l.Coordinates).IsRequired();

                entity.HasOne(l => l.Zone)
                .WithMany(l => l.Locations)
                .HasForeignKey(l => l.ZoneId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(l => l.ParkingTickets)
                .WithOne(l => l.Location)
                .HasForeignKey(l => l.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.PaymentDate).IsRequired();
                entity.Property(p => p.TransactionNumber).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Amount).IsRequired().HasColumnType("decimal(18,2)");

                entity.HasOne(p => p.ParkingTicket)
                .WithMany(p => p.Payments)
                .HasForeignKey(p => p.ParkingTicketId)
                .OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<ParkingTicket>(entity =>
            {
                entity.HasKey(pt => pt.Id);

                entity.Property(pt => pt.TicketNumber).IsRequired().HasMaxLength(30);
                entity.Property(pt => pt.Amount).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(pt => pt.IssuedAt).IsRequired();
                entity.Property(pt => pt.Status).IsRequired().HasConversion<string>();

                entity.HasOne(pt => pt.Vehicle)
                .WithMany(pt => pt.ParkingTickets)
                .HasForeignKey(pt => pt.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(pt => pt.Location)
                .WithMany(pt => pt.ParkingTickets)
                .HasForeignKey(pt => pt.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(pt => pt.Worker)
                .WithMany(pt => pt.IssuedTickets)
                .HasForeignKey(pt => pt.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
