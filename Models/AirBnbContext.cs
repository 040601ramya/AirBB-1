using System;                      
using Microsoft.EntityFrameworkCore;

namespace AirBB.Models
{
    public class AirBnbContext : DbContext
    {
        public AirBnbContext(DbContextOptions<AirBnbContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Residence> Residences { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Experience> Experiences { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, City = "Chicago" },
                new Location { LocationId = 2, City = "New York" },
                new Location { LocationId = 3, City = "Miami" }
            );

          
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    ClientId = 1,
                    Name = "Demo User",
                    PhoneNumber = "000-000-0000",
                    Email = "demo@example.com",
                    DOB = DateTime.Parse("1990-01-01")
                }
            );

           
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
                    BuiltYear = 2010,
                    PricePerNight = 120m
                },
                new Residence
                {
                    ResidenceId = 2,
                    Name = "NYC Loft",
                    ResidencePicture = "newyork.jpg",
                    LocationId = 2,
                    GuestNumber = 4,
                    BedroomNumber = 2,
                    BathroomNumber = 2,
                    BuiltYear = 2012,
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
                    BuiltYear = 2015,
                    PricePerNight = 250m
                }
            );

            
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    ReservationId = 1,
                    ResidenceId = 1,
                    ClientId = 1,
                    ReservationStartDate = DateTime.Parse("2025-01-05"),
                    ReservationEndDate = DateTime.Parse("2025-01-08"),
                    TotalPrice = 120m * 3
                },
                new Reservation
                {
                    ReservationId = 2,
                    ResidenceId = 2,
                    ClientId = 1,
                    ReservationStartDate = DateTime.Parse("2025-02-10"),
                    ReservationEndDate = DateTime.Parse("2025-02-12"),
                    TotalPrice = 200m * 2
                },
                new Reservation
                {
                    ReservationId = 3,
                    ResidenceId = 3,
                    ClientId = 1,
                    ReservationStartDate = DateTime.Parse("2025-03-01"),
                    ReservationEndDate = DateTime.Parse("2025-03-05"),
                    TotalPrice = 250m * 4
                }
            );
        }
    }
}
