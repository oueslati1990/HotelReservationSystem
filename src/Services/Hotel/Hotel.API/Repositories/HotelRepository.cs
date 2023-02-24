using Hotel.API.Data;
using Hotel.API.Entities;
using Hotel.API.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.API.Repositories
{
    public class HotelRepository : IRepository<HotelEntity>
    {
        private readonly DataContext _context;

        public HotelRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<HotelEntity> GetByIdAsync(int id)
        {
            return await _context.Hotels.FindAsync(id);
        }

        public async Task<HotelEntity> CreateAsync(HotelEntity entity)
        {
            await _context.Hotels.AddAsync(entity);
            var created = await _context.SaveChangesAsync();
            return created > 0 ? await GetByIdAsync(_context.Hotels.Count() - 1)
                               : null;
        }

        public async Task<bool> UpdateAsync(HotelEntity hotelToUpdate)
        {
            _context.Hotels.Update(hotelToUpdate);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var hotel = await GetByIdAsync(id);

            _context.Hotels.Remove(hotel);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

    }
}
