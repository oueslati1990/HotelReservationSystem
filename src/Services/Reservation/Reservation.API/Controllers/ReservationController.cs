using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reservation.API.DTOs;
using Reservation.API.Entities;
using Reservation.API.Enums;
using Reservation.API.Helpers;
using Reservation.API.Interfaces;

namespace Reservation.API.Controllers
{
    [ApiController]
    [Route("v1/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomInventoryRepository _roomInventoryRepository;

        public ReservationController(ILogger<ReservationController> logger ,
            IReservationRepository reservationRepository,
            IRoomInventoryRepository roomInventoryRepository)
        {
            _logger = logger;
            _reservationRepository = reservationRepository;
            _roomInventoryRepository = roomInventoryRepository;
        }

        [HttpPost("reserve")]
        public async Task<ActionResult<ReservationResponseDto>> ReserveRoom([FromBody] ReservationRequestDto reservationRequestDto)
        {
            //1. Validate Model
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(m => m.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();

                return BadRequest(errors);
            }

            _logger.LogInformation($"ReservationRequestDto Model is valid ");

            //2. Create a new reservation
            var canReserve = await _roomInventoryRepository.CanReserve(reservationRequestDto);
            if (!canReserve) return BadRequest("Hotel is full , you can't reserve for this date !");

            var response = await _reservationRepository.CreateReservation(reservationRequestDto);
            if (response == null) return BadRequest("Something went wrong when creating reservation !");

            _logger.LogInformation($"Reservation was created with ID= {response.ReservationId}");

            //3. Modify Rooms inventory
            var inventoryUpdated = await _roomInventoryRepository.Update(reservationRequestDto);
            if (!inventoryUpdated) return BadRequest("Inventory was not updated , please retry reservation !");

            return Ok(response);
        }

        [HttpPost("complete/{reservationId}")]
        public ActionResult<bool> CompleteReservation(Guid reservationId)
        {
            //1. Get reservation

            //2. pay for the reservation

            //3. Modify reservation status
            return null;
        }

        [HttpPost("cancel/{reservationId}")]
        public ActionResult<bool> CancelReservation(Guid reservationId)
        {
            //1. Get reservation

            //2. pay refund 

            //3. Modify reservation status
            return null;
        }
       
    }
}