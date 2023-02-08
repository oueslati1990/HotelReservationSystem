using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.API.DTOs
{
    public class HotelRequestDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
    }
}
