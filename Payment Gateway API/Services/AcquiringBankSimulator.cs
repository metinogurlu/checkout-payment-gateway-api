using PaymentGatewayAPI.Entities;

namespace PaymentGatewayAPI.Services
{
    public class AcquiringBankSimulator : IAcquiringBankSimulator
    {
        public ResponseCode ProcessPayment(ProcessPaymentRequest paymentRequest)
        {
            return ResponseCodes.Approved;
        }
    }
}