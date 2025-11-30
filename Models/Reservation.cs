using System;

namespace HotelReservation.Models
{
    public class Reservation
    {
        public int Id { get; set; }  

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public string GuestName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public decimal Total { get; set; }
        public bool Paid { get; set; } = false;
    }
}
