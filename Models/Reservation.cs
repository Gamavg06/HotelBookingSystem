using System;

namespace HotelReservation.Models
{
    public class Reservation
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign Key
        public int RoomId { get; set; }

        // Navigation Property — ESTA ES LA QUE TE FALTABA
        public Room Room { get; set; }

        public string GuestName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public decimal Total { get; set; }
        public bool Paid { get; set; } = false;
    }
}
