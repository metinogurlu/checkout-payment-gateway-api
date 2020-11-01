using PaymentGateway.API.Entities;

namespace PaymentGateway.API.Validators
{
    public interface IProcessPaymentRequestValidator
    {
        bool isValid(ProcessPaymentRequest processPaymentRequest);

        bool IsCardValid(Card card);

        bool isAmountValid(decimal amount);

        bool isCurrencyValid(string currencyCode);

        ResponseCode ValidateCard(Card card);
    }
}