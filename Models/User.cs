using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }   // Clave primaria necesaria para EF Core

        // Datos del usuario
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public int Age { get; set; }
        public required string Telephone { get; set; }
        public required string Email { get; set; }
        public required string Nationality { get; set; }

        // Seguridad
        public required string Password { get; set; }

        // Rol para permisos
        public string Role { get; set; } = "User";

        // 👇 Navegación hacia las reservaciones
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
