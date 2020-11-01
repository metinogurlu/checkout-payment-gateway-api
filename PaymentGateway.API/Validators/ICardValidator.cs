using PaymentGateway.API.Entities;

namespace PaymentGateway.API.Validators
{
    public interface ICardValidator
    {
        bool isValid(Card card);

        bool isCardNumberValid(string cardNumber);

        bool isExpiryDateValid(int month, int year);

        bool isCvvValid(string cvv);
    }
}