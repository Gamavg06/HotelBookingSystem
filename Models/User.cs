using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }   // Clave primaria necesaria para EF Core

        // Datos del usuario
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public int Edad { get; set; }
        public required string Celular { get; set; }
        public required string Email { get; set; }
        public required string Nacionalidad { get; set; }

        // Seguridad
        public required string Password { get; set; }

        // Rol para permisos
        public string Role { get; set; } = "User";
    }
}
