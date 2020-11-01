using PaymentGateway.API.Entities;
using System;
using System.Linq;

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
                CardNumber = GetMaskedCardNumber(paymentRequest.Card.CardNumber),
                Amount = paymentRequest.Amount,
                Currency = paymentRequest.Currency,
                ResponseCode = ResponseCodes.Approved.Code,
                ResponseSummary = ResponseCodes.Approved.Message,
                Status = "Successful",
                ProcessedAt = DateTime.Now,
            };
        }

        /// <summary>
        /// Hide card number except last four character
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        private string GetMaskedCardNumber(string cardNumber)
        {
            string firstPartOfCardNumber = cardNumber[0..^4];
            return cardNumber.Replace(firstPartOfCardNumber, string.Concat(Enumerable.Repeat("*", firstPartOfCardNumber.Length)));
        }
    }
}