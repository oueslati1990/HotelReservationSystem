using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.API.Entities
{
    public class RoomTypeInventory
    {
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public DateTime Date { get; set; }
        public int TotalInventory { get; set; }
        public int TotalReserved { get; set; }
    }
}
