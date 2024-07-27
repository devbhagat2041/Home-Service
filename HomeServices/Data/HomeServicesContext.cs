using Microsoft.EntityFrameworkCore;
using HomeServices.Models;

namespace HomeServices.Data
{
    public class HomeServicesContext : DbContext
    {
        public HomeServicesContext(DbContextOptions<HomeServicesContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<ServiceCategory> ServiceCategory { get; set; }
        public DbSet<HomeServices.Models.ServiceProvider> ServiceProvider { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Rating> Rating { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.City)
                .WithMany()
                .HasForeignKey(u => u.CityId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Country)
                .WithMany()
                .HasForeignKey(u => u.CountryId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserType)
                .WithMany()
                .HasForeignKey(u => u.UserTypeId);

            modelBuilder.Entity<HomeServices.Models.ServiceProvider>()
                .HasOne(sp => sp.User)
                .WithMany()
                .HasForeignKey(sp => sp.UserId);

            modelBuilder.Entity<HomeServices.Models.ServiceProvider>()
                .HasOne(sp => sp.ServiceCategory)
                .WithMany()
                .HasForeignKey(sp => sp.ServiceCategoryId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany()
                .HasForeignKey(b => b.CustomerId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.ServiceProvider)
                .WithMany()
                .HasForeignKey(b => b.ServiceProviderId);

            modelBuilder.Entity<Booking>()
       .HasOne(b => b.Status)
       .WithMany()
       .HasForeignKey(b => b.StatusId);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Booking)
                .WithMany()
                .HasForeignKey(r => r.BookingId);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.ServiceProvider)
                .WithMany()
                .HasForeignKey(r => r.ServiceProviderId);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.ServiceCategory)
                .WithMany()
                .HasForeignKey(r => r.ServiceCategoryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
