using Microsoft.EntityFrameworkCore;
using HotelReservation.Models;

namespace HotelReservation.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
        }

        // Tablas
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -------------------------
            // CONFIGURACIÓN DE USER
            // -------------------------
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasDefaultValue("User");

            // -------------------------
            // SEED: ADMIN POR DEFECTO
            // -------------------------
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    LastName = "System",
                    Email = "hotelbookingsystems@gmail.com",
                    Password = "Admin",
                    Age = 30,
                    Telephone = "2761095688",
                    Nationality = "MX",
                    Role = "Admin"
                }
            );

            // -------------------------
            // CONFIGURACIÓN DE ROOM
            // -------------------------
            modelBuilder.Entity<Room>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Room>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Room>()
                .Property(r => r.Description)
                .HasMaxLength(500);

            // -------------------------
            // SEED DE HABITACIONES
            // -------------------------
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Standard Room", Description = "Double Bed, 20m².", PricePerNight = 340, Image = "/images/room1.png" },
                new Room { Id = 2, Name = "Superior Room", Description = "King Bed, Balcony.", PricePerNight = 350, Image = "/images/room2.png" },
                new Room { Id = 3, Name = "Suite", Description = "Living room, King Bed, Views.", PricePerNight = 2500, Image = "/images/room3.png" },
                new Room { Id = 4, Name = "Family Room", Description = "Spacious, 2 beds.", PricePerNight = 500, Image = "/images/room4.png" }
            );

            // -------------------------
            // CONFIGURACIÓN DE RESERVATION
            // -------------------------
            modelBuilder.Entity<Reservation>()
                .HasKey(res => res.Id);

            modelBuilder.Entity<Reservation>()
                .Property(res => res.GuestName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Reservation>()
                .Property(res => res.Email)
                .IsRequired();

            // Relación Room (1) → (N) Reservations
            modelBuilder.Entity<Reservation>()
                .HasOne(res => res.Room)
                .WithMany(r => r.Reservations)
                .HasForeignKey(res => res.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación User (1) → (N) Reservations
            modelBuilder.Entity<Reservation>()
                .HasOne(res => res.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(res => res.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
