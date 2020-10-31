using System;

namespace PaymentGatewayAPI.Entities
{
    public class AcquiringBankResponse
    {
        public Guid Id { get; set; }
        public ProcessPaymentRequest PaymentRequest { get; set; }
        public ResponseCode ResponseCode { get; set; }

        public AcquiringBankResponse(ProcessPaymentRequest paymentRequest, ResponseCode responseCode)
        {
            Id = Guid.NewGuid();
            PaymentRequest = paymentRequest;
            ResponseCode = responseCode;
        }
    }
}