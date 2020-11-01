using PaymentGateway.API.Entities;

namespace PaymentGateway.API.Services
{
    public interface IAcquiringBankSimulator
    {
        public Payment ProcessPayment(ProcessPaymentRequest paymentRequest);
    }
}