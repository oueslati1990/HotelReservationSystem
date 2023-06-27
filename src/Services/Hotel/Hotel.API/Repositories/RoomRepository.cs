using Hotel.API.Data;
using Hotel.API.Entities;
using Hotel.API.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Hotel.API.DTOs;

namespace Hotel.API.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DataContext _context;

        public RoomRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Room> GetByIdAsync(Guid id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<Room> CreateAsync(Room entity)
        {
            _context.Rooms.Add(entity);
            var created = await _context.SaveChangesAsync();

            return created > 0 ? await GetByIdAsync(entity.RoomId)
                               : null;
        }

        public async Task<bool> UpdateAsync(Room room , RoomRequestDto roomRequestDto)
        {
            room.RoomTypeId = roomRequestDto.RoomTypeId;
            room.Floor = roomRequestDto.Floor;
            room.Number = roomRequestDto.Number;
            room.Name = roomRequestDto.Name;
            room.IsAvailable = roomRequestDto.IsAvailable;

            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(Room room)
        {
            _context.Rooms.Remove(room);
            var removed = await _context.SaveChangesAsync();
            return removed > 0;
        }
      
    }
}
