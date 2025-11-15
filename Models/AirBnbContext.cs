using Microsoft.EntityFrameworkCore;

namespace AirBB.Models
{
    public class AirBnbContext : DbContext
    {
        public AirBnbContext(DbContextOptions<AirBnbContext> options) : base(options) { }

        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Residence> Residences => Set<Residence>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Client> Clients => Set<Client>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary Keys
            modelBuilder.Entity<Location>().HasKey(x => x.LocationId);
            modelBuilder.Entity<Residence>().HasKey(x => x.ResidenceId);
            modelBuilder.Entity<Reservation>().HasKey(x => x.ReservationId);
            modelBuilder.Entity<Client>().HasKey(x => x.UserId);

            // Relationships
            modelBuilder.Entity<Residence>()
                .HasOne(r => r.Location)
                .WithMany()
                .HasForeignKey(r => r.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Residence)
                .WithMany()
                .HasForeignKey(r => r.ResidenceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Client)
                .WithMany()
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Locations
            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, Name = "Chicago" },
                new Location { LocationId = 2, Name = "New York" },
                new Location { LocationId = 3, Name = "Miami" }
            );

            // Seed Residences (Updated image names)
            modelBuilder.Entity<Residence>().HasData(
                new Residence
                {
                    ResidenceId = 1,
                    Name = "Chicago Loop Apartment",
                    ResidencePicture = "chicago.jpg",
                    LocationId = 1,
                    GuestNumber = 3,
                    BedroomNumber = 2,
                    BathroomNumber = 1,
                    PricePerNight = 120m
                },
                new Residence
                {
                    ResidenceId = 2,
                    Name = "New York City Loft",
                    ResidencePicture = "newyork.jpg",
                    LocationId = 2,
                    GuestNumber = 4,
                    BedroomNumber = 2,
                    BathroomNumber = 2,
                    PricePerNight = 200m
                },
                new Residence
                {
                    ResidenceId = 3,
                    Name = "Miami Beach House",
                    ResidencePicture = "miami.jpeg",
                    LocationId = 3,
                    GuestNumber = 6,
                    BedroomNumber = 3,
                    BathroomNumber = 2,
                    PricePerNight = 250m
                }
            );

            // Seed Client
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    UserId = 1,
                    Name = "Demo User",
                    PhoneNumber = "000-000-0000",
                    Email = "demo@example.com",
                    DOB = new System.DateTime(1990, 1, 1)
                }
            );
        }
    }
}