using PaymentGatewayAPI.Data;
using PaymentGatewayAPI.Entities;

namespace PaymentGatewayAPI.Services
{
    public interface IPaymentService
    {
        Payment ProcessPayment(ProcessPaymentRequest paymentRequest);

        ResponseCode ValidatePaymentRequest(ProcessPaymentRequest processPaymentRequest);
    }
}