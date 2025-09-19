using BarberBookingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberBookingApp.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<BarberService> BarberServices { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Barber)
                .WithMany(b => b.Appointments)
                .HasForeignKey(a => a.BarberId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Barber>()
                .HasOne(b => b.User)
                .WithOne()
                .HasForeignKey<Barber>(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BarberService>()
                .HasKey(bs => new { bs.BarberId, bs.ServiceId }); 
            modelBuilder.Entity<BarberService>()
                .HasOne(bs => bs.Barber)
                .WithMany(b => b.BarberServices)
                .HasForeignKey(bs => bs.BarberId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BarberService>()
                .HasOne(bs => bs.Service)
                .WithMany(s => s.BarberServices)
                .HasForeignKey(bs => bs.ServiceId);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Customer>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Barber)
                .WithMany(b => b.Schedules)
                .HasForeignKey(s => s.BarberId)
                .OnDelete(DeleteBehavior.Cascade);

            // Konfiguracja precyzji wartości pieniężnych (2 miejsca po przecinku)
            modelBuilder.Entity<Appointment>()
                .Property(a => a.Price)
                .HasPrecision(18, 2);
            
            modelBuilder.Entity<BarberService>()
                .Property(bs => bs.Price)
                .HasPrecision(18, 2);
            
            modelBuilder.Entity<Service>()
                .Property(s => s.BasePrice)
                .HasPrecision(18, 2);
        }
    }
}
