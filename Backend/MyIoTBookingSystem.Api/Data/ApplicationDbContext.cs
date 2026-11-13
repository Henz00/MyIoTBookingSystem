using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MyIoTBookingSystem.Api.Models;

namespace MyIoTBookingSystem.Api.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<HomeImage> HomeImages { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Home
            // Datatype constraint setup
            modelBuilder.Entity<Home>()
                .Property(home => home.DailyPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Home>()
                .Property(home => home.Name)
                .HasMaxLength(150);

            modelBuilder.Entity<Home>()
                .Property(home => home.Description)
                .HasMaxLength(1500);

            modelBuilder.Entity<Home>()
                .Property(home => home.StreetName)
                .HasMaxLength(150);

            modelBuilder.Entity<Home>()
                .Property(home => home.StreetNumber)
                .HasMaxLength(16);

            modelBuilder.Entity<Home>()
                .Property(home => home.City)
                .HasMaxLength(100);

            modelBuilder.Entity<Home>()
                .Property(home => home.ZipNumber)
                .HasMaxLength(10);

            // Foreign key setup
            modelBuilder.Entity<Home>()
                .HasOne(home => home.Department)
                .WithMany()
                .HasForeignKey(home => home.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Home>()
                .HasMany(home => home.Images)
                .WithOne()
                .HasForeignKey(image => image.HomeId)
                .OnDelete(DeleteBehavior.Cascade);


            // ApplicationUser
            // Datatype constraint setup
            modelBuilder.Entity<ApplicationUser>()
                .Property(user => user.FullName)
                .HasMaxLength(150);


            // Department
            // Datatype constraint setup
            modelBuilder.Entity<Department>()
                .Property(department => department.Name)
                .HasMaxLength(150);


            // Booking
            // Datatype constraint setup
            modelBuilder.Entity<Booking>()
                .Property(booking => booking.AgreedDailyPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Booking>()
                .Property(booking => booking.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Booking>()
                .Property(booking => booking.CustomerName)
                .HasMaxLength(150);

            modelBuilder.Entity<Booking>()
                .Property(booking => booking.CancellationReason)
                .HasMaxLength(1000);

            modelBuilder.Entity<Booking>()
                .Property(booking => booking.CustomerEmail)
                .HasMaxLength(250);

            modelBuilder.Entity<Booking>()
                .Property(booking => booking.CustomerPhone)
                .HasMaxLength(32);

            // Foreign key setup

            modelBuilder.Entity<Booking>()
                .HasOne(booking => booking.Customer)
                .WithMany()
                .HasForeignKey(booking => booking.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(booking => booking.Home)
                .WithMany()
                .HasForeignKey(booking => booking.HomeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(booking => booking.CreatedByUser)
                .WithMany()
                .HasForeignKey(booking => booking.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(booking => booking.CancelledByUser)
                .WithMany()
                .HasForeignKey(booking => booking.CancelledByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
