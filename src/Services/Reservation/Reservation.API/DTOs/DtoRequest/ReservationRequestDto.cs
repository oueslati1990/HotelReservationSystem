using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.API.DTOs
{
    public class ReservationRequestDto
    {
        [Required]
        public Guid HotelId { get; set; }
        [Required]
        public int RoomTypeId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public Guid GuestId { get; set; }
    }
}
