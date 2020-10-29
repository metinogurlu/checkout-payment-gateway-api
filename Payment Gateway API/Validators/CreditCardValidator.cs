using CreditCardValidator;
using PaymentGatewayAPI.Entities;
using System;
using System.Text.RegularExpressions;

namespace PaymentGatewayAPI.Validators
{
    public class CreditCardValidator : ICreditCardValidator
    {
        public bool isValid(Card card)
            => isCardNumberValid(card.CardNumber) && isExpiryDateValid(card.ExpirationMonth, card.ExpirationYear) && isCvvValid(card.Cvv);

        public bool isCardNumberValid(string cardNumber)
            => new CreditCardDetector(cardNumber).IsValid();

        public bool isCvvValid(string cvv)
            => new Regex(@"^\d{3}$").IsMatch(cvv);

        public bool isExpiryDateValid(int month, int year)
        {
            var lastDateOfExpiryMonth = DateTime.DaysInMonth(year, month);
            var cardExpiry = new DateTime(year, month, lastDateOfExpiryMonth, 23, 59, 59);

            //check expiry greater than today & goes 20 years into the future.
            return (cardExpiry > DateTime.Now && cardExpiry < DateTime.Now.AddYears(20));
        }
    }
}