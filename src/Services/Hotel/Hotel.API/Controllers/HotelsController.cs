using AutoMapper;
using Hotel.API.DTOs;
using Hotel.API.Entities;
using Hotel.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hotel.API.Controllers
{
    [ApiController]
    [Route("v1/hotels")]
    public class HotelsController : ControllerBase
    {
        private readonly IRepository<HotelEntity> _repo;
        private readonly IMapper _mapper;

        public HotelsController(IRepository<HotelEntity> repo , IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK , Type = typeof(HotelResponseDto))]
        public async Task<ActionResult<HotelResponseDto>> GetHotelById(int id)
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
            var newHotel = new HotelEntity
            {
                Name = hotelRequest.Name,
                Address = hotelRequest.Address,
                Location = hotelRequest.Location
            };

            var createdHotel = await _repo.CreateAsync(newHotel);
            if (createdHotel == null) return BadRequest("Hotel was not created !");
            
            var hotelToReturn = _mapper.Map<HotelResponseDto>(createdHotel);
            return Ok(hotelToReturn);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest , Type = typeof(string))]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelEntity hotel)
        {
            var updated = await _repo.UpdateAsync(hotel);
            if (!updated) return BadRequest("Hotel was not updated ! ");

            return Ok();
        }

        //[Authorize(Policy = "UserPolicy")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest , Type = typeof(string))]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _repo.GetByIdAsync(id);
            if (hotel == null) return NotFound("Hotel doesn't exist!");

            var deleted = await _repo.DeleteAsync(hotel);
            if (!deleted) return BadRequest(" Hotel was not deleted !");

            return Ok();
        }
    }
}
