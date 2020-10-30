using System.Linq;
using PaymentGatewayAPI.Entities;

namespace PaymentGatewayAPI.Validators
{
    public class ProcessPaymentRequestValidator : IProcessPaymentRequestValidator
    {
        private readonly string[] _currencies = new string[]
        { "AFN", "DZD", "ARS", "AMD", "AWG", "AUD", "AZN", "BSD", "BHD", "THB", "PAB",
          "BBD", "BYN", "BZD", "BMD", "VEF", "BOB", "BRL", "BND", "BGN", "BIF", "CAD",
          "CVE", "KYD", "GHS", "XOF", "XAF", "XPF", "CLP", "COP", "KMF", "CDF", "BAM",
          "NIO", "CRC", "HRK", "CUP", "CZK", "GMD", "DKK", "MKD", "DJF", "STD", "DOP",
          "VND", "XCD", "EGP", "SVC", "ETB", "EUR", "FKP", "FJD", "HUF", "GIP", "HTG",
          "PYG", "GNF", "GYD", "HKD", "UAH", "ISK", "INR", "IRR", "IQD", "JMD", "JOD",
          "KES", "PGK", "LAK", "EEK", "KWD", "MWK", "AOA", "MMK", "GEL", "LVL", "LBP",
          "ALL", "HNL", "SLL", "LRD", "LYD", "SZL", "LTL", "LSL", "MGA", "MYR", "TMT",
          "MUR", "MZN", "MXN", "MDL", "MAD", "NGN", "ERN", "NAD", "NPR", "ANG", "ILS",
          "RON", "TWD", "NZD", "BTN", "KPW", "NOK", "PEN", "MRO", "TOP", "PKR", "MOP",
          "UYU", "PHP", "GBP", "BWP", "QAR", "GTQ", "ZAR", "OMR", "KHR", "MVR", "IDR",
          "RUB", "RWF", "SHP", "SAR", "RSD", "SCR", "SGD", "SBD", "KGS", "SOS", "TJS",
          "LKR", "SDG", "SRD", "SEK", "CHF", "SYP", "BDT", "WST", "TZS", "KZT", "TTD",
          "MNT", "TND", "TRY", "AED", "UGX", "CLF", "USD", "UZS", "VUV", "KRW", "YER",
          "JPY", "CNY", "ZMW", "ZWL", "PLN" };

        private readonly ICardValidator _cardValidator;

        public ProcessPaymentRequestValidator(ICardValidator cardValidator)
        {
            _cardValidator = cardValidator;
        }

        public bool isAmountValid(decimal amount) => amount > 0;

        public bool isCurrencyValid(string currencyCode)
        {
            return _currencies.Contains(currencyCode);
        }

        public bool IsCardValid(Card card) => ValidateCard(card) == ResponseCodes.Approved;

        public bool isValid(ProcessPaymentRequest processPaymentRequest) =>
            isAmountValid(processPaymentRequest.Amount)
            && isCurrencyValid(processPaymentRequest.Currency)
            && IsCardValid(processPaymentRequest.Card);

        public ResponseCode ValidateCard(Card card)
        {
            if (_cardValidator.isValid(card))
                return ResponseCodes.Approved;

            if (!_cardValidator.isCardNumberValid(card.CardNumber))
                return ResponseCodes.SoftDecline.InvalidCardNumber;
            else if (!_cardValidator.isExpiryDateValid(card.ExpirationMonth, card.ExpirationYear))
                return ResponseCodes.SoftDecline.BadTrackData;
            else
                return ResponseCodes.RiskResponses.CvvMissingOrIncorrect;
        }
    }
}