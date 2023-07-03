using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.API.DTOs
{
    public class ReservationResponseDto
    {
        public Guid ReservationId { get; set; }
        public Guid HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public Guid GuestId { get; set; }
    }
}
