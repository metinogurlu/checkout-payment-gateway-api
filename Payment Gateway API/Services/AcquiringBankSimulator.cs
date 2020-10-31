using PaymentGatewayAPI.Entities;

namespace PaymentGatewayAPI.Services
{
    public class AcquiringBankSimulator : IAcquiringBankSimulator
    {
        public AcquiringBankResponse ProcessPayment(ProcessPaymentRequest paymentRequest)
        {
            return new AcquiringBankResponse(paymentRequest, ResponseCodes.Approved);
        }
    }
}