using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }   // Clave primaria necesaria para EF Core

        // Datos del usuario
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Nacionalidad { get; set; }

        // Seguridad
        public string Password { get; set; }

        // Rol para permisos
        public string Role { get; set; } = "User";
    }
}
