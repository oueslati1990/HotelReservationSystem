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
    [Route("v1/hotels/{hotelId}/rooms")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository _roomsRepo;
        private readonly IHotelRepository _hotelsRepo;
        private readonly IMapper _mapper;

        public RoomsController(IRoomRepository roomsRepo, IHotelRepository hotelsRepo, IMapper mapper)
        {
            _roomsRepo = roomsRepo;
            _hotelsRepo = hotelsRepo;
            _mapper = mapper;
        }

        [HttpGet("{roomId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomResponseDto))]
        public async Task<ActionResult<RoomResponseDto>> GetRoomById(Guid hotelId, Guid roomId)
        {
            var hotel = await _hotelsRepo.GetByIdAsync(hotelId);
            if (hotel == null) return NotFound("Hotel not found ! ");

            var room = await _roomsRepo.GetByIdAsync(roomId);
            if (room == null) return NotFound("Room not found !");

            var roomToReturn = _mapper.Map<RoomResponseDto>(room);
            return Ok(roomToReturn);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomResponseDto))]
        public async Task<ActionResult<RoomResponseDto>> CreateRoomAsync([FromBody] RoomRequestDto roomRequest, Guid hotelId)
        {
            if(!ModelState.IsValid) 
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
                return BadRequest(errors);
            }

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

        [HttpPut("{roomId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateRoomAsync([FromBody] RoomRequestDto roomRequestDto, Guid hotelId, Guid roomId)
        {
            if(!ModelState.IsValid) 
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
                return BadRequest(errors);
            }
            
            var hotel = await _hotelsRepo.GetByIdAsync(hotelId);
            if (hotel == null) return NotFound("Hotel doesn't exist !");

            var room = await _roomsRepo.GetByIdAsync(roomId);
            if (room == null) return NotFound("Room not found !");
          
            var isUpdated = await _roomsRepo.UpdateAsync(room, roomRequestDto);
            if (!isUpdated) return BadRequest("Room wasn't updated ! ");

            return Ok();
        }

        [HttpDelete("{roomId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRoomAsync(Guid hotelId , Guid roomId)
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
