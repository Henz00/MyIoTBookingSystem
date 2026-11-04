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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<Department>()
                .Property(department => department.Name)
                .HasMaxLength(150);

            modelBuilder.Entity<ApplicationUser>()
                .Property(user => user.FullName)
                .HasMaxLength(150);

            // Foreign key setup
            modelBuilder.Entity<Home>()
                .HasOne(home => home.Department)
                .WithMany()
                .HasForeignKey(home => home.DepartmentId);

            modelBuilder.Entity<Home>()
                .HasMany(home => home.Images)
                .WithOne()
                .HasForeignKey(image => image.HomeId)
                .OnDelete(DeleteBehavior.Cascade);
     
        }
    }
}
