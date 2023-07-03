using AutoMapper;
using Reservation.API.DTOs;
using Reservation.API.Entities;

namespace Reservation.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ReservationEntity, ReservationResponseDto>();
        }
    }
}
