using PaymentGateway.API.Entities;
using PaymentGateway.API.Helpers;
using System;

namespace PaymentGateway.API.Services
{
    public class AcquiringBankSimulator : IAcquiringBankSimulator
    {
        /// <summary>
        /// Simulate bank's payment process behavior
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        public Payment ProcessPayment(ProcessPaymentRequest paymentRequest)
        {
            return new Payment
            {
                ProcessId = Guid.NewGuid(),
                CardNumber = CardHelper.GetMaskedCardNumber(paymentRequest.Card.CardNumber),
                Amount = paymentRequest.Amount,
                Currency = paymentRequest.Currency,
                ResponseCode = ResponseCodes.Approved.Code,
                ResponseSummary = ResponseCodes.Approved.Message,
                Status = "Successful",
                ProcessedAt = DateTime.Now,
            };
        }
    }
}