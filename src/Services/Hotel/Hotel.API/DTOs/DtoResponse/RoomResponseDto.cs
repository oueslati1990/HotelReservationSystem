using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.API.DTOs
{
    public class RoomResponseDto
    {
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
        public int HotelId { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
    }
}
