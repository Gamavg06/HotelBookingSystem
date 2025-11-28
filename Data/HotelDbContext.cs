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
                    Celular = "0000000000",
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
                .HasOne<Room>()
                .WithMany()
                .HasForeignKey(res => res.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
