using System;

namespace Reservation.API.Entities
{
    public class RoomTypeInventory
    {
        public Guid HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public DateTime Date { get; set; }
        public int TotalInventory { get; set; }
        public int TotalReserved { get; set; }
    }
}
