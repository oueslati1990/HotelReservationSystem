using Hotel.API.Data;
using Hotel.API.Entities;
using Hotel.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Hotel.API.DTOs;

namespace Hotel.API.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly DataContext _context;

        public HotelRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<HotelEntity> GetByIdAsync(Guid id)
        {
            return await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
        }

        public async Task<HotelEntity> CreateAsync(HotelEntity entity)
        {
            await _context.Hotels.AddAsync(entity);
            var created = await _context.SaveChangesAsync();
            return created > 0 ? await GetByIdAsync(entity.HotelId)
                               : null;
        }

        public async Task<bool> UpdateAsync(HotelEntity hotel,HotelRequestDto hotelDto)
        {
            hotel.Name = hotelDto.Name;
            hotel.Address = hotelDto.Address;
            hotel.Location = hotelDto.Location;
            
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(HotelEntity hotel)
        {
            _context.Hotels.Remove(hotel);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

    }
}
