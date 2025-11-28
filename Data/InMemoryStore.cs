using System.Collections.Generic;
using HotelReservation.Models;

namespace HotelReservation.Data
{
    public class InMemoryStore
    {
        public List<Room> Rooms { get; } = new List<Room>();
        public List<Reservation> Reservations { get; } = new List<Reservation>();

        public List<User> Users { get; } = new List<User>();

        public InMemoryStore()
        {
            Rooms.Add(new Room { Id = 1, Name = "Habitación Estándar", Description = "Cama doble, 20m².", PricePerNight = 340, Image = "/images/room1.png" });
            Rooms.Add(new Room { Id = 2, Name = "Habitación Superior", Description = "Cama king, balcón.", PricePerNight = 350, Image = "/images/room2.png" });
            Rooms.Add(new Room { Id = 3, Name = "Suite", Description = "Sala, cama king, vistas.", PricePerNight = 2500, Image = "/images/room3.png" });
            Rooms.Add(new Room { Id = 4, Name = "Habitación Familiar", Description = "Espaciosa, 2 camas.", PricePerNight = 500, Image = "/images/room4.png" });
        }
    }
}
