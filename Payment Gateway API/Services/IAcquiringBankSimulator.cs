using PaymentGatewayAPI.Entities;

namespace PaymentGatewayAPI.Services
{
    public interface IAcquiringBankSimulator
    {
        public ResponseCode ProcessPayment(ProcessPaymentRequest paymentRequest);
    }
}