using Reservation.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.API.Interfaces
{
    public interface IRoomInventoryRepository
    {
        Task<bool> Update(ReservationRequestDto reservationRequestDto);
        Task<bool> CanReserve(ReservationRequestDto reservationRequestDto);
    }
}
