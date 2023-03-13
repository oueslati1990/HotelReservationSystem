using Hotel.API.Data;
using Hotel.API.Entities;
using Hotel.API.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.API.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private readonly DataContext _context;

        public RoomRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Room> GetByIdAsync(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<Room> CreateAsync(Room entity)
        {
            _context.Rooms.Add(entity);
            var created = await _context.SaveChangesAsync();

            return created > 0 ? await GetByIdAsync(_context.Rooms.Count() - 1)
                               : null;
        }

        public async Task<bool> UpdateAsync(Room entity)
        {
            _context.Rooms.Update(entity);
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
