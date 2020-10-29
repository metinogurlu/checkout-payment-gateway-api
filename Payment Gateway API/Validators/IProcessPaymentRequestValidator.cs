using PaymentGatewayAPI.Entities;

namespace PaymentGatewayAPI.Validators
{
    public interface IProcessPaymentRequestValidator
    {
        bool isValid(ProcessPaymentRequest processPaymentRequest);

        bool IsCardValid(Card card);

        bool isAmountValid(decimal amount);

        bool isCurrencyValid(string currencyCode);
    }
}