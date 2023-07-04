using Reservation.API.DTOs;
using Reservation.API.Entities;
using Reservation.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.API.Interfaces
{
    public interface IReservationRepository
    {
        Task<ReservationResponseDto> CreateReservation(ReservationRequestDto reservationRequestDto);
        Task<ReservationEntity> GetReservationById(Guid id);
        Task<ReservationEntity> UpdateReservation(ReservationEntity reservation , Status status);
    }
}
