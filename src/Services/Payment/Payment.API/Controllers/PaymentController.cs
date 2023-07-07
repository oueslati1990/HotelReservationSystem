using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment.API.Entities;
using Payment.API.Enums;
using Payment.API.Helpers;
using Payment.API.Interfaces;
using Stripe;

namespace Payment.API.Controllers
{
    [ApiController]
    [Route("v1/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(Reservation reservation)
        {
            try
            {
                var payment = new PaymentResponse
                {
                    ReservationId = reservation.ReservationId,
                    Status = EnumHelper.GetDescription(Status.PENDING),
                    Message = string.Empty
                };

                bool isReservationPaid = await _paymentRepository.IsReservationPaid(reservation.ReservationId);
                if (isReservationPaid)
                {
                    payment.Message = "Reservation has already been paid.";
                }

                var paymentResponse = await _paymentRepository.PayReservation(reservation);

                payment.Status = paymentResponse.Status;
                payment.Message = paymentResponse.Message;

                return Ok(payment); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing payment: {ex.Message}");
            }
        }

        [HttpPut("{reservationId}/refund")]
        public async Task<ActionResult<PaymentResponse>> RefundPayment(Guid reservationId)
        {
            try
            {
                var refund  = new PaymentResponse
                {
                    ReservationId = reservationId,
                    Status = EnumHelper.GetDescription(Status.PENDING),
                    Message = string.Empty
                };
            
                // Check if the reservation has already been refunded
                bool isReservationRefunded = await _paymentRepository.IsReservationRefunded(reservationId);
                if (isReservationRefunded)
                {
                    refund.Message = "Reservation has already been refunded.";
                }

                var refundResponse = await _paymentRepository.PayRefund(reservationId);

                refund.Status = refundResponse.Status;
                refund.Message = refundResponse.Message;

                return Ok(refund); 
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate error response
                return StatusCode(500, $"Error processing refund: {ex.Message}");
            }
        }
    }
}
