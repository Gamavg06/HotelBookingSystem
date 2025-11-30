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
                .Property(u => u.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.Apellido)
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
                    Nombre = "Admin",
                    Apellido = "System",
                    Email = "hotelbookingsystems@gmail.com",
                    Password = "Admin",
                    Edad = 30,
                    Celular = "2761095688",
                    Nacionalidad = "MX",
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
                new Room { Id = 1, Name = "Habitación Estándar", Description = "Cama doble, 20m².", PricePerNight = 340, Image = "/images/room1.png" },
                new Room { Id = 2, Name = "Habitación Superior", Description = "Cama king, balcón.", PricePerNight = 350, Image = "/images/room2.png" },
                new Room { Id = 3, Name = "Suite", Description = "Sala, cama king, vistas.", PricePerNight = 2500, Image = "/images/room3.png" },
                new Room { Id = 4, Name = "Habitación Familiar", Description = "Espaciosa, 2 camas.", PricePerNight = 500, Image = "/images/room4.png" }
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

            // Relación Room (1) → (N) Reservations - AHORA CORREGIDA
            modelBuilder.Entity<Reservation>()
                .HasOne(res => res.Room)          // Usa la propiedad de navegación en Reservation
                .WithMany(r => r.Reservations)    // Usa la colección de navegación en Room
                .HasForeignKey(res => res.RoomId) // Usa la clave foránea real
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}