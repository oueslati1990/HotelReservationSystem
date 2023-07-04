using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reservation.API.Data;
using Reservation.API.DTOs;
using Reservation.API.Entities;
using Reservation.API.Enums;
using Reservation.API.Helpers;
using Reservation.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.API.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationContext _context;
        private readonly IMapper _mapper;

        public ReservationRepository(ReservationContext context , IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ReservationResponseDto> CreateReservation(ReservationRequestDto reservationRequestDto)
        {
            var reservation = new ReservationEntity
            {
                ReservationId = Guid.NewGuid(),
                HotelId = reservationRequestDto.HotelId,
                RoomTypeId = reservationRequestDto.RoomTypeId,
                StartDate = reservationRequestDto.StartDate,
                EndDate = reservationRequestDto.EndDate,
                Status = EnumHelper.GetDescription(Status.PENDING),
                GuestId = reservationRequestDto.GuestId
            };

            _context.Reservations.Add(reservation);
            var created = await _context.SaveChangesAsync();
            if (created <= 0) return null;

            return _mapper.Map<ReservationResponseDto>(reservation);
        }

        public async Task<ReservationEntity> GetReservationById(Guid id)
        {
            return await _context.Reservations.FirstOrDefaultAsync(r => r.ReservationId == id);
        }

        public async Task<ReservationEntity> UpdateReservation(ReservationEntity reservation, Status status)
        {
            reservation.Status = EnumHelper.GetDescription(status);
            var updated = await _context.SaveChangesAsync() > 0;

            return updated ? reservation : null;
        }
    }
}
