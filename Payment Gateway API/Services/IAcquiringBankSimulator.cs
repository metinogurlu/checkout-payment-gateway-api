using PaymentGatewayAPI.Data;
using PaymentGatewayAPI.Entities;

namespace PaymentGatewayAPI.Services
{
    public interface IAcquiringBankSimulator
    {
        public Payment ProcessPayment(ProcessPaymentRequest paymentRequest);
    }
}