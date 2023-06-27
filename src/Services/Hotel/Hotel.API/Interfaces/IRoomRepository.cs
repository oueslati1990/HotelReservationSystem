using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.API.Entities;
using Hotel.API.DTOs;


namespace Hotel.API.Interfaces
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<bool> UpdateAsync(Room room , RoomRequestDto roomRequestDto);
    }
}