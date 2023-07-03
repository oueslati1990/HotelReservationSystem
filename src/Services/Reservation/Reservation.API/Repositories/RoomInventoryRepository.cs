using Microsoft.EntityFrameworkCore;
using Reservation.API.Data;
using Reservation.API.DTOs;
using Reservation.API.Interfaces;
using System;
using System.Threading.Tasks;

namespace Reservation.API.Repositories
{
    public class RoomInventoryRepository : IRoomInventoryRepository
    {
        private readonly ReservationContext _context;

        public RoomInventoryRepository(ReservationContext context)
        {
            _context = context;
        }

        public async Task<bool> CanReserve(ReservationRequestDto reservationRequestDto)
        {
            DateTime date = reservationRequestDto.StartDate;
            while (date <= reservationRequestDto.EndDate)
            {
                var inventory = await _context.RoomTypeInventories.FirstOrDefaultAsync(r => r.HotelId == reservationRequestDto.HotelId
                                                                         && r.RoomTypeId == reservationRequestDto.RoomTypeId
                                                                         && r.Date == date);
                
                if (inventory.TotalInventory == inventory.TotalReserved) return false;
            }

            return true;
        }

        public async Task<bool> Update(ReservationRequestDto reservationRequestDto)
        {
            DateTime date = reservationRequestDto.StartDate;
            while (date <= reservationRequestDto.EndDate)
            {
                var inventory = await _context.RoomTypeInventories.FirstOrDefaultAsync(r => r.HotelId == reservationRequestDto.HotelId
                                                                         && r.RoomTypeId == reservationRequestDto.RoomTypeId
                                                                         && r.Date == date);
                
                if (!await CanReserve(reservationRequestDto))
                    return false;

                inventory.TotalReserved++;
                inventory.TotalInventory--;
            }

            var updated = await _context.SaveChangesAsync() > 0;

            return updated;
        }
    }
}
