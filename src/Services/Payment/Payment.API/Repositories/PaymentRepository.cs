﻿using Payment.API.Entities;
using Payment.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stripe;
using Payment.API.Helpers;
using Payment.API.Enums;
using Payment.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Payment.API.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentContext _context;

        public PaymentRepository(PaymentContext context)
        {
            _context = context;
        }

        public async Task<bool> IsReservationPaid(Guid reservationId)
        {
            var payment =  await _context.PaymentDatas.FindAsync(reservationId);
            if (payment == null) return false;

            return payment.IsPaid;
        }

        public async Task<PaymentResponse> PayReservation(Reservation reservation)
        {
            // Create payment charge using Stripe API
            var chargeOptions = new ChargeCreateOptions
            {
                Amount = 1000, // Replace with the actual payment amount
                Currency = "usd", // Replace with the actual currency
                Description = "Hotel Reservation Payment",
                Source = "tok_visa", // Replace with the actual payment source (e.g., token generated by Stripe.js)
                Metadata = new Dictionary<string, string>
                    {
                        { "ReservationId", reservation.ReservationId.ToString() },
                        { "GuestId", reservation.GuestId.ToString() }
                    }
            };

            var chargeService = new ChargeService();
            var charge = await chargeService.CreateAsync(chargeOptions);

            if (charge == null || charge.Paid == false)
                return new PaymentResponse
                {
                    ReservationId = reservation.ReservationId,
                    Status = EnumHelper.GetDescription(Status.PENDING),
                    Message = $"Reservation {reservation.ReservationId} was not paid"
                };

            var paymentData = new PaymentData
            {
                ReservationId = reservation.ReservationId,
                GuestId = reservation.GuestId,
                IsPaid = true,
                PaidAt = DateTime.Now,
                GotRefund = false,
                GotRefundAt = DateTime.MinValue,
                ChargeId = charge.Id,
                Amount = chargeOptions.Amount,
                Currency = chargeOptions.Currency,
                Source = chargeOptions.Source,
                Description = chargeOptions.Description
            };

            _context.PaymentDatas.Add(paymentData);
            await _context.SaveChangesAsync();

            return new PaymentResponse
            {
                ReservationId = reservation.ReservationId,
                Status = EnumHelper.GetDescription(Status.PAID)
            };
        }

        public async Task<bool> IsReservationRefunded(Guid reservationId)
        {
            var payment = await _context.PaymentDatas.FirstOrDefaultAsync(p=> p.ReservationId == reservationId);
            if (payment == null) return false;

            return payment.GotRefund;
        }

        public async Task<PaymentResponse> PayRefund(Guid reservationId)
        {
            var paymentResponse = new PaymentResponse
            {
                ReservationId = reservationId,
                Status = EnumHelper.GetDescription(Status.PAID),
                Message = string.Empty
            };

            
            var payment = await _context.PaymentDatas.FirstOrDefaultAsync(p => p.ReservationId == reservationId);
            if (payment == null)
                paymentResponse.Message = $"No payment with the reservation Id {reservationId}";

            
            // Refund the payment charge using Stripe API
            var refundOptions = new RefundCreateOptions
            {
                Charge = payment.ChargeId
            };

            var refundService = new RefundService();
            var refund = await refundService.CreateAsync(refundOptions);

            payment.GotRefund = true;
            payment.GotRefundAt = DateTime.Now;
            await _context.SaveChangesAsync();

            paymentResponse.Status = EnumHelper.GetDescription(Status.CANCELED);

            return paymentResponse;
        }
    }
}
