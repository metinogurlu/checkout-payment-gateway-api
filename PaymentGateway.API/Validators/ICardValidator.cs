using PaymentGateway.API.Entities;

namespace PaymentGateway.API.Validators
{
    public interface ICardValidator
    {
        bool IsValid(Card card);

        bool IsCardNumberValid(string cardNumber);

        bool IsExpiryDateValid(int month, int year);

        bool IsCvvValid(string cvv);
    }
}