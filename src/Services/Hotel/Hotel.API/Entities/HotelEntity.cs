using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.API.Entities
{
    public class HotelEntity
    {
        public HotelEntity()
        {
            Rooms = new HashSet<Room>();
        }

        [Key]
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
