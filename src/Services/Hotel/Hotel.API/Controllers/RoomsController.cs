using AutoMapper;
using Hotel.API.DTOs;
using Hotel.API.Entities;
using Hotel.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.API.Controllers
{
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRepository<Room> _roomsRepo;
        private readonly IRepository<HotelEntity> _hotelsRepo;
        private readonly IMapper _mapper;

        public RoomsController(IRepository<Room> roomsRepo, IRepository<HotelEntity> hotelsRepo, IMapper mapper)
        {
            _roomsRepo = roomsRepo;
            _hotelsRepo = hotelsRepo;
            _mapper = mapper;
        }

        [HttpGet("v1/hotels/{hotelId}/rooms/{roomId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomResponseDto))]
        public async Task<ActionResult<RoomResponseDto>> GetHotelById(int hotelId, int roomId)
        {
            var hotel = await _hotelsRepo.GetByIdAsync(hotelId);
            if (hotel == null) return NotFound("Hotel not found ! ");

            var room = await _roomsRepo.GetByIdAsync(roomId);
            if (room == null) return NotFound("Room not found !");

            var roomToReturn = _mapper.Map<RoomResponseDto>(room);
            return Ok(roomToReturn);
        }

        [HttpPost("v1/hotels/{hotelId}/rooms")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomResponseDto))]
        public async Task<ActionResult<RoomResponseDto>> CreateAsync([FromBody] RoomRequestDto roomRequest, int hotelId)
        {
            var hotel = await _hotelsRepo.GetByIdAsync(hotelId);
            if (hotel == null) return NotFound("Hotel doesn't exist !");

            var room = new Room
            {
                HotelId = hotelId,
                RoomTypeId = roomRequest.RoomTypeId,
                Floor = roomRequest.Floor,
                Number = roomRequest.Number,
                Name = roomRequest.Name,
                IsAvailable = roomRequest.IsAvailable
            };

            var createdRoom = await _roomsRepo.CreateAsync(room);

            var roomToReturn = _mapper.Map<RoomResponseDto>(createdRoom);
            return Ok(roomToReturn);
        }

        [HttpPut("v1/hotels/{hotelId}/rooms/{roomId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] RoomRequestDto roomRequestDto, int hotelId, int roomId)
        {
            var hotel = await _hotelsRepo.GetByIdAsync(hotelId);
            if (hotel == null) return NotFound("Hotel doesn't exist !");

            var room = await _roomsRepo.GetByIdAsync(roomId);
            if (room == null) return NotFound("Room not found !");

            room.RoomTypeId = roomRequestDto.RoomTypeId;
            room.Floor = roomRequestDto.Floor;
            room.Number = roomRequestDto.Number;
            room.Name = roomRequestDto.Name;
            room.IsAvailable = roomRequestDto.IsAvailable;

            var isUpdated = await _roomsRepo.UpdateAsync(room);
            if (!isUpdated) return BadRequest("Room wasn't updated ! ");

            return Ok();
        }

        [HttpDelete("v1/hotels/{hotelId}/rooms/{roomId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(int hotelId , int roomId)
        {
            var hotel = await _hotelsRepo.GetByIdAsync(hotelId);
            if (hotel == null) return NotFound("Hotel doesn't exist !");

            var room = await _roomsRepo.GetByIdAsync(roomId);
            if (room == null) return NotFound("Room doesn't exist !");

            bool isDeleted = await _roomsRepo.DeleteAsync(room);
            if (!isDeleted) return BadRequest("Room wasn't deleted");

            return Ok();
        }
    }
}
