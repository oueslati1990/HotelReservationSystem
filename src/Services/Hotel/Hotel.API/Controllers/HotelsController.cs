using AutoMapper;
using Hotel.API.DTOs;
using Hotel.API.Entities;
using Hotel.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.API.Controllers
{
    [ApiController]
    [Route("v1/hotels")]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _repo;
        private readonly IMapper _mapper;

        public HotelsController(IHotelRepository repo , IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK , Type = typeof(HotelResponseDto))]
        public async Task<ActionResult<HotelResponseDto>> GetHotelById(Guid id)
        {
            var hotel = await _repo.GetByIdAsync(id);

            if (hotel == null) return NotFound("Hotel not found ! ");

            var hotelToReturn = _mapper.Map<HotelResponseDto>(hotel);
            return Ok(hotelToReturn);
        }

        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK , Type =typeof(HotelResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest , Type = typeof(string))]
        public async Task<ActionResult<HotelResponseDto>> CreateHotel([FromBody] HotelRequestDto hotelRequest)
        {
            if(!ModelState.IsValid) 
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
                return BadRequest(errors);
            }
            
            var newHotel = new HotelEntity
            {
                HotelId = Guid.NewGuid(),
                Name = hotelRequest.Name,
                Address = hotelRequest.Address,
                Location = hotelRequest.Location
            };

            var createdHotel = await _repo.CreateAsync(newHotel);
            if (createdHotel == null) return BadRequest("Hotel was not created !");
            
            var hotelToReturn = _mapper.Map<HotelResponseDto>(createdHotel);
            return Ok(hotelToReturn);
        }

        [HttpPut("hotelId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest , Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound , Type = typeof(string))]
        public async Task<IActionResult> UpdateHotel(Guid hotelId, [FromBody] HotelRequestDto hotelRequestDto)
        {
            if(!ModelState.IsValid) 
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
                return BadRequest(errors);
            }
            
            var hotel = await _repo.GetByIdAsync(hotelId);
            if(hotel == null) return NotFound("Hotel was not found!");
            
            var updated = await _repo.UpdateAsync(hotel, hotelRequestDto);
            if (!updated) return BadRequest("Hotel was not updated ! ");

            return Ok();
        }

        //[Authorize(Policy = "UserPolicy")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest , Type = typeof(string))]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            var hotel = await _repo.GetByIdAsync(id);
            if (hotel == null) return NotFound("Hotel doesn't exist!");

            var deleted = await _repo.DeleteAsync(hotel);
            if (!deleted) return BadRequest(" Hotel was not deleted !");

            return Ok();
        }
    }
}
