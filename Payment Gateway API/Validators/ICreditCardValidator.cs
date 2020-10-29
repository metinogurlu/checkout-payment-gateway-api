using PaymentGatewayAPI.Entities;

namespace PaymentGatewayAPI.Validators
{
    public interface ICreditCardValidator
    {
        bool isValid(Card card);

        bool isCardNumberValid(string cardNumber);

        bool isExpiryDateValid(int month, int year);

        bool isCvvValid(string cvv);
    }
}