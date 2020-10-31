using PaymentGatewayAPI.Entities;
using System;
using System.Linq;

namespace PaymentGatewayAPI.Services
{
    public class AcquiringBankSimulator : IAcquiringBankSimulator
    {
        public Payment ProcessPayment(ProcessPaymentRequest paymentRequest)
        {
            return new Payment
            {
                ProcessId = Guid.NewGuid(),
                CardNumber = GetMaskedCardNumber(paymentRequest.Card.CardNumber),
                Amount = paymentRequest.Amount,
                Currency = paymentRequest.Currency,
                ResponseCode = ResponseCodes.Approved.Code,
                ResponseSummary = ResponseCodes.Approved.Message,
                Status = "Successful",
                ProcessedAt = DateTime.Now,
            };
        }

        private string GetMaskedCardNumber(string cardNumber)
        {
            string firstPartOfCardNumber = cardNumber[0..^4];
            return cardNumber.Replace(firstPartOfCardNumber, string.Concat(Enumerable.Repeat("*", firstPartOfCardNumber.Length)));
        }
    }
}