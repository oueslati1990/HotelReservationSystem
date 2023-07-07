using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Entities
{
    public class PaymentData
    {
        public Guid ReservationId { get; set; }
        public Guid GuestId { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PaidAt { get; set; }
        public bool GotRefund { get; set; }
        public DateTime GotRefundAt { get; set; }
        public string ChargeId { get; set; }
        public long? Amount { get; set; }
        public string Currency { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
    }
}
