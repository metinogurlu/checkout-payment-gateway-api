using PaymentGatewayAPI.Entities;

namespace PaymentGatewayAPI.Services
{
    public interface IPaymentService
    {
        ResponseCode ProcessPayment(ProcessPaymentRequest processPaymentRequest);

        ResponseCode ValidatePaymentRequest(ProcessPaymentRequest processPaymentRequest);
    }
}