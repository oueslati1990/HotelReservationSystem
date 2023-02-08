using AutoMapper;
using Hotel.API.DTOs;
using Hotel.API.Entities;

namespace Hotel.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<HotelEntity , HotelResponseDto>();
            CreateMap<Room , RoomResponseDto>();
        }
    }
}
