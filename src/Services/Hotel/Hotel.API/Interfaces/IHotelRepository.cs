using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.API.DTOs;
using Hotel.API.Entities;

namespace Hotel.API.Interfaces
{
    public interface IHotelRepository : IRepository<HotelEntity>
    {
        Task<bool> UpdateAsync(HotelEntity hotel,HotelRequestDto hotelDto);
    }
}