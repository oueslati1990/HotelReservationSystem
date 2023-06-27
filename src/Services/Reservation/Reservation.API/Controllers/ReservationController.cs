using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reservation.API.DTOs;
 

namespace Reservation.API.Controllers
{
    [ApiController]
    [Route("v1/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(ILogger<ReservationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("reserve")]
        public ActionResult<ReservationReadDto> ReserveRoom([FromBody] ReservationWriteDto reservationWriteDto)
        {
            return null;
        }
       
    }
}