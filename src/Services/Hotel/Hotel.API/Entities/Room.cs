using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.API.Entities
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
        public int HotelId { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }

        public virtual HotelEntity HotelEntity { get; set; }
        public virtual RoomType RoomType { get; set; }
    }
}
