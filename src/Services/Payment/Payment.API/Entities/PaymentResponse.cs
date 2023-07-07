using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Entities
{
    public class PaymentResponse
    {
        public Guid ReservationId { get; set; }
        public String Status { get; set; }
        public String Message { get; set; }
    }
}
