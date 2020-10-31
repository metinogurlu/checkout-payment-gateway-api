using PaymentGatewayAPI.Entities;

namespace PaymentGatewayAPI.Services
{
    public interface IAcquiringBankSimulator
    {
        public AcquiringBankResponse ProcessPayment(ProcessPaymentRequest paymentRequest);
    }
}