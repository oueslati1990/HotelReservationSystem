using Payment.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Interfaces
{
    public interface IPaymentRepository
    {
        Task<bool> IsReservationPaid(Guid reservationId);
        Task<PaymentResponse> PayReservation(Reservation reservation);
        Task<bool> IsReservationRefunded(Guid reservationId);
        Task<PaymentResponse> PayRefund(Guid reservationId);
    }
}
