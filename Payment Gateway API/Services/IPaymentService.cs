using PaymentGatewayAPI.Entities;

namespace PaymentGatewayAPI.Services
{
    public interface IPaymentService
    {
        ProcessPaymentResponse ProcessPayment(ProcessPaymentRequest paymentRequest);

        ResponseCode ValidatePaymentRequest(ProcessPaymentRequest processPaymentRequest);
    }
}