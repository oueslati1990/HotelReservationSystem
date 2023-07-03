using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hotel.API.DTOs
{
    public class RoomRequestDto
    {
        [Required]
        public int RoomTypeId { get; set; }
        [Required]
        public int Floor { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
    }
}
