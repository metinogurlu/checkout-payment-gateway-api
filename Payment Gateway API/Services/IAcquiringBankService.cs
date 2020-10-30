using PaymentGatewayAPI.Entities;

namespace PaymentGatewayAPI.Services
{
    public interface IAcquiringBankService
    {
        public ProcessPaymentResponse ProcessPayment(ProcessPaymentRequest paymentRequest);
    }
}