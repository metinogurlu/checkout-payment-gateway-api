using PaymentGateway.API.Entities;

namespace PaymentGateway.API.Validators
{
    public interface IProcessPaymentRequestValidator
    {
        bool IsValid(ProcessPaymentRequest processPaymentRequest);

        bool IsCardValid(Card card);

        bool IsAmountValid(decimal amount);

        bool IsCurrencyValid(string currencyCode);

        ResponseCode ValidateCard(Card card);
    }
}