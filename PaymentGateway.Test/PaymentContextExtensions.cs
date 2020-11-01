using PaymentGateway.API.Data;
using PaymentGateway.API.Entities;
using System;

namespace PaymentGateway.Test
{
    public static class PaymentContextExtensions
    {
        public static void Seed(this PaymentContext paymentContext)
        {
            paymentContext.Payments.Add(new Payment
            {
                ProcessId = Guid.Parse("c1583bf7-ee44-4006-8bb6-52b7d5a07643"),
                CardNumber = "************4242",
                Amount = 100,
                Currency = "USD",
                ProcessedAt = DateTime.Now.AddDays(-1),
                ResponseCode = ResponseCodes.Approved.Code,
                ResponseSummary = ResponseCodes.Approved.Message,
                Status = "Successful",
            });

            paymentContext.SaveChanges();
        }
    }
}