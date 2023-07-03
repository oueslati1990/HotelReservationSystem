using Reservation.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.API.Interfaces
{
    public interface IReservationRepository
    {
        Task<ReservationResponseDto> CreateReservation(ReservationRequestDto reservationRequestDto);
    }
}
